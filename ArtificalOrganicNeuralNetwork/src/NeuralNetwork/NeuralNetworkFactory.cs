using System;
using System.Collections.Generic;
using System.Linq;
using AONN.NN.Neurons;

namespace AONN.NN
{
    public class NeuralNetworkFactory
    {
        private const double ZeroStrength = 0.01;

        public static NeuralNetwork CreateNeuralNetwork(NeuralNetworkCreationConfig config)
        {
            var neuroTransmitterSet = new NeuroTransmitterSet(config.NeuroTransmitterCount);
            var computingNeurons = new ComputingNeuron[config.ComputingNeuronCount];


            for (int i = 0; i < config.ComputingNeuronCount; i++)
            {
                computingNeurons[i] = new ComputingNeuron($"C{i}", config);
            }

            var receivingNeurons =
                computingNeurons.Cast<IReceivingNeuron>().Concat(config.OutputNeurons.Cast<IReceivingNeuron>()).ToArray();

            for (int i = 0; i < config.ComputingNeuronCount; i++)
            {
                CreateRandomSynapses(
                    config, 
                    neuroTransmitterSet, 
                    computingNeurons[i], 
                    receivingNeurons
                );
            }

            for (int i = 0; i < config.InputNeurons.Count(); i++)
            {
                CreateRandomSynapses(
                    config, 
                    neuroTransmitterSet, 
                    config.InputNeurons[i], 
                    receivingNeurons
                );
            }


            return new NeuralNetwork(config, computingNeurons, config.InputNeurons, config.OutputNeurons);
        }

        private static void CreateRandomSynapses(
            NeuralNetworkCreationConfig config, 
            NeuroTransmitterSet neuroTransmitterSet,
            INeuron neuron, 
            IReceivingNeuron[] receivingNeurons
        )
        {
            var count = Math.Round(RandomGaussian(config.CreationRand, config.SynapseCountMean, config.SynapseCountStdDev));
            var receivingNeuronsCount = receivingNeurons.Length;
            var synapses = new List<Synapse>();

            for (int i = 0; i < count; i++)
            {
                var receivingNeuron = receivingNeurons[config.CreationRand.Next(receivingNeuronsCount)];
                var sourceAffinities = CreateRandomTransmitterAffinities(config, neuroTransmitterSet);
                var targetAffinities = CreateRandomTransmitterAffinities(config, neuroTransmitterSet);
                var strength = RandomGaussian(config.CreationRand, config.SynapseStrengthMean, config.SynapseStrengthStdDev);
                if (strength < ZeroStrength)
                {
                    strength = ZeroStrength;
                }
                var synapse = new Synapse(
                    strength, 
                    neuron, receivingNeuron, 
                    sourceAffinities, targetAffinities,
                    neuroTransmitterSet
                    );
                synapses.Add(synapse);
            }

            neuron.Synapses = synapses;
        }

        private static IDictionary<NeuroTransmitter, double> CreateRandomTransmitterAffinities(NeuralNetworkCreationConfig config, NeuroTransmitterSet neuroTransmitterSet)
        {
            var affinities = new Dictionary<NeuroTransmitter, double>();

            foreach (var transmitter in neuroTransmitterSet.Transmitters)
            {
                affinities[transmitter] = config.CreationRand.NextDouble();
            }

            return affinities;
        }

        private static double RandomGaussian(Random rand, double mean, double stdDev)
        {
            double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                   Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal = mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)

            return randNormal;
        }
    }
}
