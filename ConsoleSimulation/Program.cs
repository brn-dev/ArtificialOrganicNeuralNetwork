using AONN.NN;
using AONN.NN.Neurons;
using System;
using System.Threading;

namespace ConsoleSimulation
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var r = new Random();

            var config = NeuralNetworkCreationConfig.Builder(13053631, r.Next(0, 99999999))
                .BuildNeuralNetworkConfig(configBuilder => configBuilder
                        .NeuroTransmitterCount(1)
                        .PotentialLossPerTick(0.999999999999)
                        .LowerPotentialThreshold(1)
                        .UpperPotentialThreshold(2)
                )
                .ComputingNeuronCount(0)
                .SynapseCountMean(2)
                .SynapseCountStdDev(0)
                .InitialSynapseStrength(1)
                .Build();

            var subject = ConsoleOrganismFactory.CreateConsoleOrgansim(
                config,
                (o, c) => new InputNeuron[]
                {
                    new BooleanConstantInputNeuron("I1", c, 10, () => c.Rand.NextDouble() >= 0.5),
                    new BooleanConstantInputNeuron("I2", c, 10, () => c.Rand.NextDouble() >= 0.5),
                },
                (o, c) => new OutputNeuron[]
                {
                    new OutputNeuron("O1", c, () => o.AddMomentum(new Vector2d(0, 0.5))),
                    new OutputNeuron("O2", c, () => o.AddAngularMomentum(0.2)),
                }
            ); 


            var world = new World();

            world.Entities.Add(subject);
            world.Entities.Add(new FoodEntity(new Vector2i(3, 3)));

            var tick = 0L;

            var renderer = new Renderer(world, subject);

            while (true)
            {
                subject.Tick(tick);
                tick++;

                renderer.RenderNow(tick, config.CreationSeed, config.NeuralNetworkConfig.Seed);


                Thread.Sleep(20);
            }
        }
    }
}
