using AONN.NN;
using AONN.NN.Neurons;
using System;
using System.Threading;
using System.Linq;

namespace ConsoleSimulation
{
    class Program
    {
        delegate int SeedProvider();


        private const bool _simulateSingle = true;

        private const int _creationSeed = 1810860249;
        private const int _seed = 612640322;

        private const int _threadCount = 20;
        private const int _maxTicks = 10000;

        private const long _ticksPerSecond = 1000;
        private const int _fps = 50;

        static void Main(string[] args)
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


            Processor.ConfigProvider configInitializer =
                () => new NeuralNetworkCreationConfig(creationSeedProvider(), seedProvider())
                {
                    // runtime config
                    NeuroTransmitterCount = 1,
                    PotentialLossPerTick = 0.999999999999,
                    LowerPotentialThreshold = 1,
                    UpperPotentialThreshold = 2,

                    // creation config
                    ComputingNeuronCount = 10,
                    SynapseCountMean = 5,
                    SynapseCountStdDev = 0.5,
                    SynapseStrengthMean = 10,
                    SynapseStrengthStdDev = 3
                };

            ConsoleOrganismFactory.InputNeuronInitializer inputNeuronInitializer =
                (o, c) => new InputNeuron[]
                    {
                        new BooleanConstantInputNeuron("I1", c, 0.1, () => c.Rand.NextDouble() >= 0.05),
                        new BooleanConstantInputNeuron("I2", c, 0.1, () => c.Rand.NextDouble() >= 0.5),
                    };

            ConsoleOrganismFactory.OutputNeuronInitializer outputNeuronInitializer =
                (o, c) =>
                    new OutputNeuron[0]
                        .Concat(OutputNeuron.Times(10, "O-F", c, () => o.AddMomentum(new Vector2d(0, 0.02))))
                        .Concat(OutputNeuron.Times(10, "O-B", c, () => o.AddMomentum(new Vector2d(0, -0.02))))
                        .Concat(OutputNeuron.Times(3, "O-R", c, () => o.AddAngularMomentum(0.01)))
                        .Concat(OutputNeuron.Times(3, "O-L", c, () => o.AddAngularMomentum(-0.01)))
                        .ToArray();

            if (_simulateSingle)
            {
                SimulateSingle(configInitializer, inputNeuronInitializer, outputNeuronInitializer);
            }
            else
            {
                SimulateMultiple(configInitializer, inputNeuronInitializer, outputNeuronInitializer);
            }
        }

        private static void SimulateMultiple(
            Processor.ConfigProvider configInitializer,
            ConsoleOrganismFactory.InputNeuronInitializer inputNeuronInitializer,
            ConsoleOrganismFactory.OutputNeuronInitializer outputNeuronInitializer)
        {
            var previousDone = -1;

            var (tasks, subjects) = Processor.ProcessMultiple(_threadCount, configInitializer, inputNeuronInitializer, outputNeuronInitializer, _maxTicks);

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
                        Console.WriteLine($"{config.CreationSeed}/{config.Seed}: {subject.FoodCollected}, {DidMove(subject)}");
                    }
                    break;
                }

                Thread.Sleep(20);
            }

        }

        private static void SimulateSingle(
            Processor.ConfigProvider configInitializer,
            ConsoleOrganismFactory.InputNeuronInitializer inputNeuronInitializer,
            ConsoleOrganismFactory.OutputNeuronInitializer outputNeuronInitializer
            )
        {
            var (task, subject) = Processor.CreateSubjectAndProcessWithLimiter(
                configInitializer(),
                inputNeuronInitializer,
                outputNeuronInitializer,
                _maxTicks,
                _ticksPerSecond
                );

            var world = subject.World;

            world.Entities.Add(new FoodEntity(new Vector2i(0, 3)));

            var renderer = new Renderer(world, subject);

            renderer.RenderAtFps(_fps);
        }

        private static bool DidMove(ConsoleOrganism consoleOrganism)
        {
            return consoleOrganism.Position.X != 0 || consoleOrganism.Position.Y != 0;
        }
    }
}
