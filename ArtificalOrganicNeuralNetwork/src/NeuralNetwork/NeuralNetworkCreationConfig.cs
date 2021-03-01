using System;
using AONN.NN.Neurons;

namespace AONN.NN
{
    public class NeuralNetworkCreationConfig
    {
        public NeuralNetworkCreationConfig(int creationSeed, NeuralNetworkConfig neuralNetworkConfig)
        {
            CreationSeed = creationSeed;
            CreationRand = new Random(creationSeed);
            NeuralNetworkConfig = neuralNetworkConfig;
        }

        public int CreationSeed { get; }

        public Random CreationRand { get; }

        public NeuralNetworkConfig NeuralNetworkConfig { get; set; }


        public int ComputingNeuronCount { get; set; }

        public InputNeuron[] InputNeurons { get; set; }

        public OutputNeuron[] OutputNeurons { get; set; }

        public double SynapseCountMean { get; set; }

        public double SynapseCountStdDev { get; set; }

        public double InitialSynapseStrength { get; set; }

        public static NeuralNetworkCreationConfigBuilder Builder(int creationSeed, int seed)
        {
            return new NeuralNetworkCreationConfigBuilder(creationSeed, seed);
        }

        public class NeuralNetworkCreationConfigBuilder
        {
            private int _creationSeed;

            private int _seed;

            private NeuralNetworkConfig _neuralNetworkConfig;

            private int _computingNeuronCount;

            private double _synapseCountMean;

            private double _synapseCountStdDev;

            private double _initialSynapseStrength;

            public NeuralNetworkCreationConfigBuilder(int creationSeed, int seed)
            {
                _creationSeed = creationSeed;
                _seed = seed;
            }

            public NeuralNetworkCreationConfigBuilder BuildNeuralNetworkConfig(
                Action<NeuralNetworkConfig.NeuralNetworkConfigBuilder> configInitializer
                ) 
            {
                var configBuilder = NeuralNetworkConfig.Builder(_seed);
                configInitializer(configBuilder);
                _neuralNetworkConfig = configBuilder.Build();
                return this;
            }

            public NeuralNetworkCreationConfigBuilder ComputingNeuronCount(int computingNeuronCount)
            {
                _computingNeuronCount = computingNeuronCount;
                return this;
            }

            public NeuralNetworkCreationConfigBuilder SynapseCountMean(double synapseCountMean)
            {
                _synapseCountMean = synapseCountMean;
                return this;
            }

            public NeuralNetworkCreationConfigBuilder SynapseCountStdDev(double synapseCountStdDev)
            {
                _synapseCountStdDev = synapseCountStdDev;
                return this;
            }

            public NeuralNetworkCreationConfigBuilder InitialSynapseStrength(double initialSynapseStrength)
            {
                _initialSynapseStrength = initialSynapseStrength;
                return this;
            }

            public NeuralNetworkCreationConfig Build()
            {
                if (_neuralNetworkConfig == null)
                {
                    throw new InvalidOperationException("Cannot build NeuralNetworkCreationConfig without a NeuralNetworkConfig. Call BuildNeuralNetworkConfig(...)!");
                }

                var config = new NeuralNetworkCreationConfig(_creationSeed, _neuralNetworkConfig)
                {
                    ComputingNeuronCount = _computingNeuronCount,
                    SynapseCountMean = _synapseCountMean,
                    SynapseCountStdDev = _synapseCountStdDev,
                    InitialSynapseStrength = _initialSynapseStrength
                };

                return config;
            }
        }

    }
}
