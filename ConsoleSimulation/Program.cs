using AONN.NN.Configs;
using AONN.NN.Neurons;
using System;
using System.Threading;
using System.Linq;
using ConsoleSimulation.Entities;

namespace ConsoleSimulation
{
    class Program
    {
        delegate int SeedProvider();

        private const bool _simulateSingle = true;
        private const bool _printConfigOfSingle = false;

        private const int _creationSeed = 1478597184;
        private const int _seed = -680533558;

        private const int _threadCount = 20;

        private const int _maxTicks = 10000;

        private const long _ticksPerSecond = 400;
        private const int _fps = 50;

        static void Main()
        {
            var r = new Random();

            SeedProvider creationSeedProvider;
            SeedProvider seedProvider;

            if (_simulateSingle)
            {
                creationSeedProvider = () => _creationSeed;
                seedProvider = () => _seed;
            }
            else
            {
                creationSeedProvider = () => r.Next(int.MinValue, int.MaxValue);
                seedProvider = () => r.Next(int.MinValue, int.MaxValue);
            }


            Processor.ConfigProvider configProvider =
                () => new NeuralNetworkCreationConfig(creationSeedProvider(), seedProvider())
                {
                    // runtime config
                    TicksPerSuperTick = 10,
                    PotentialLossPerSuperTick = 0.999999999999,
                    LowerPotentialThreshold = 1,
                    UpperPotentialThreshold = 2,
                    StrengthGainPerTransmit = 1 + 5E-8,
                    StrengthLossPerSuperTick = 1 - 5E-10,

                    // creation config
                    ComputingNeuronCount = 10,
                    NeuroTransmitterCount = 1,
                    SynapseCount = new GaussianConfig(5, 10, 0),
                    SynapseStrength = new GaussianConfig(10, 8, 0.01)
                };

            ConsoleOrganismFactory.InputNeuronInitializer inputNeuronInitializer =
                (o, c) => new InputNeuron[]
                    {
                        new BooleanConstantInputNeuron("I1", c, 0.1, () => true),
                        new BooleanConstantInputNeuron("I2", c, 0.1, () => true),
                    };

            ConsoleOrganismFactory.OutputNeuronInitializer outputNeuronInitializer =
                (o, c) =>
                    new OutputNeuron[0]
                        .Concat(OutputNeuron.Times(10, "O-F", c, () => o.AddMomentum(new Vector2d(0, 0.02))))
                        .Concat(OutputNeuron.Times(10, "O-B", c, () => o.AddMomentum(new Vector2d(0, -0.02))))
                        .Concat(OutputNeuron.Times(3, "O-R", c, () => o.AddAngularMomentum(0.01)))
                        .Concat(OutputNeuron.Times(3, "O-L", c, () => o.AddAngularMomentum(-0.01)))
                        .ToArray();

            Processor.WorldProvider worldProvider =
                () =>
                {
                    var world = new World();

                    world.AddEntity(new FoodEntity(new Vector2i(0, 3)));

                    return world;
                };

            if (_simulateSingle)
            {
                SimulateSingle(configProvider, inputNeuronInitializer, outputNeuronInitializer, worldProvider);
            }
            else
            {
                SimulateMultiple(configProvider, inputNeuronInitializer, outputNeuronInitializer, worldProvider);
            }
        }

        private static void SimulateMultiple(
            Processor.ConfigProvider configInitializer,
            ConsoleOrganismFactory.InputNeuronInitializer inputNeuronInitializer,
            ConsoleOrganismFactory.OutputNeuronInitializer outputNeuronInitializer,
            Processor.WorldProvider worldProvider)
        {

            Console.WriteLine(int.MaxValue.ToString().Length);
            while (true)
            {
                var previousDone = -1;

                var (tasks, subjects) = Processor.ProcessMultiple(
                    _threadCount,
                    configInitializer,
                    inputNeuronInitializer,
                    outputNeuronInitializer,
                    worldProvider,
                    _maxTicks
                    );

                while (true)
                {
                    //renderer.RenderNow(tick, config.CreationSeed, config.NeuralNetworkConfig.Seed);
                    var done = tasks.Count(t => t.IsCompleted);
                    if (done > previousDone)
                    {
                        Console.WriteLine($"{done} / {_threadCount}");
                        previousDone = done;
                    }

                    if (done == _threadCount)
                    {
                        foreach (var subject in subjects)
                        {
                            var config = (NeuralNetworkCreationConfig)subject.Config;

                            var creationSeed = PadSeed(config.CreationSeed);
                            var seed = PadSeed(config.Seed);

                            Console.WriteLine($" {creationSeed} / {seed} : {subject.FoodCollected}, {DidMove(subject)}");
                        }
                        break;
                    }

                    Thread.Sleep(20);
                }
            }
        }

        private static void SimulateSingle(
            Processor.ConfigProvider configInitializer,
            ConsoleOrganismFactory.InputNeuronInitializer inputNeuronInitializer,
            ConsoleOrganismFactory.OutputNeuronInitializer outputNeuronInitializer,
            Processor.WorldProvider worldProvider
            )
        {
            var (task, subject) = Processor.CreateSubjectAndProcessWithLimiter(
                configInitializer(),
                inputNeuronInitializer,
                outputNeuronInitializer,
                worldProvider,
                _maxTicks,
                _ticksPerSecond
                );

            var world = subject.World;

            if (_printConfigOfSingle)
            {
                Console.WriteLine(subject.Config.ToString());
            }
            else
            {
                var renderer = new Renderer(world, subject);

                renderer.RenderAtFps(_fps);
            }
        }

        private static bool DidMove(ConsoleOrganism consoleOrganism)
        {
            return consoleOrganism.Position.X != 0 || consoleOrganism.Position.Y != 0;
        }

        private static string PadSeed(int seed)
        {
            if (seed >= 0)
            {
                var padded = seed.ToString().PadRight(10);
                return " " + padded;
            } 
            else
            {
                return seed.ToString().PadRight(11);
            }
        }
    }
}
