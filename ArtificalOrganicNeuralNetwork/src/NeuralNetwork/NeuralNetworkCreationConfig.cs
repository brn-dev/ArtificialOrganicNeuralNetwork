using System;
using AONN.NN.Neurons;

namespace AONN.NN
{
    public class NeuralNetworkCreationConfig : NeuralNetworkConfig
    {
        public NeuralNetworkCreationConfig(int creationSeed, int seed) : base(seed)
        {
            CreationSeed = creationSeed;
            CreationRand = new Random(creationSeed);
        }

        public int CreationSeed { get; }

        public Random CreationRand { get; }

        public int ComputingNeuronCount { get; set; }

        public InputNeuron[] InputNeurons { get; set; }

        public OutputNeuron[] OutputNeurons { get; set; }

        public double SynapseCountMean { get; set; }

        public double SynapseCountStdDev { get; set; }

        public double SynapseStrengthMean { get; set; }

        public double SynapseStrengthStdDev { get; set; }

    }
}
