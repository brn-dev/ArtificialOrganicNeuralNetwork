using System;
using System.Collections.Generic;
using System.Linq;
using AONN.NN.Neurons;

namespace AONN.NN
{
    public class NeuralNetworkFactory
    {

        public static NeuralNetwork CreateNeuralNetwork(NeuralNetworkCreationConfig config)
        {
            var neuroTransmitterSet = new NeuroTransmitterSet(config.NeuralNetworkConfig.NeuroTransmitterCount);
            var computingNeurons = new ComputingNeuron[config.ComputingNeuronCount];


            for (int i = 0; i < config.ComputingNeuronCount; i++)
            {
                computingNeurons[i] = new ComputingNeuron($"C{i}", config.NeuralNetworkConfig);
            }

            var receivingNeurons =
                computingNeurons.Cast<IReceivingNeuron>().Concat(config.OutputNeurons.Cast<IReceivingNeuron>()).ToArray();

            for (int i = 0; i < config.ComputingNeuronCount; i++)
            {
                CreateRandomSynapses(
                    config.CreationRand, 
                    neuroTransmitterSet, 
                    computingNeurons[i], 
                    receivingNeurons,
                    config.InitialSynapseStrength,
                    config.SynapseCountMean,
                    config.SynapseCountStdDev
                );
            }

            for (int i = 0; i < config.InputNeurons.Count(); i++)
            {
                CreateRandomSynapses(
                    config.CreationRand, 
                    neuroTransmitterSet, 
                    config.InputNeurons[i], 
                    receivingNeurons, 
                    config.InitialSynapseStrength,
                    config.SynapseCountMean,
                    config.SynapseCountStdDev
                );
            }


            return new NeuralNetwork(config.NeuralNetworkConfig, computingNeurons, config.InputNeurons, config.OutputNeurons);
        }

        private static void CreateRandomSynapses(
            Random rand, NeuroTransmitterSet neuroTransmitterSet,
            INeuron neuron, IReceivingNeuron[] receivingNeurons, 
            double strength, double mean, double stdDev
        )
        {
            var count = Math.Round(RandomGaussian(rand, mean, stdDev));
            var receivingNeuronsCount = receivingNeurons.Length;
            var synapses = new List<Synapse>();

            for (int i = 0; i < count; i++)
            {
                var receivingNeuron = receivingNeurons[rand.Next(receivingNeuronsCount)];
                var sourceAffinities = CreateRandomTransmitterAffinities(rand, neuroTransmitterSet);
                var targetAffinities = CreateRandomTransmitterAffinities(rand, neuroTransmitterSet);
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

        private static IDictionary<NeuroTransmitter, double> CreateRandomTransmitterAffinities(Random rand, NeuroTransmitterSet neuroTransmitterSet)
        {
            var affinities = new Dictionary<NeuroTransmitter, double>();

            foreach (var transmitter in neuroTransmitterSet.Transmitters)
            {
                affinities[transmitter] = rand.NextDouble();
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
