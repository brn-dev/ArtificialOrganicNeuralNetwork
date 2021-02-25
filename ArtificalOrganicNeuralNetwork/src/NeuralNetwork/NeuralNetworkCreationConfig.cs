using System;
using AONN.NeuralNetwork.Neurons;

namespace AONN.NeuralNetwork
{
    public class NeuralNetworkCreationConfig : NeuralNetworkConfig
    {
        public NeuralNetworkCreationConfig(int seed) : base(seed, new Random(seed))
        {
        }

        public int ComputingNeuronCount { get; set; }

        public InputNeuron[] InputNeurons { get; set; }

        public OutputNeuron[] OutputNeurons { get; set; }

        public double SynapseCountMean { get; set; }

        public double SynapseCountStdDev { get; set; }

    }
}
