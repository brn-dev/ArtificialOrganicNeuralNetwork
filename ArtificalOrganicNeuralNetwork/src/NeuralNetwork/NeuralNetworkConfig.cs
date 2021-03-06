using Newtonsoft.Json;
using System;

namespace AONN.NN
{
    public class NeuralNetworkConfig
    {
        public NeuralNetworkConfig(int seed)
        {
            Seed = seed;
            Rand = new Random(seed);
        }

        public int Seed { get; private set; }

        [JsonIgnore]
        public Random Rand { get; private set; }

        public int NeuroTransmitterCount { get; set; }

        public double LowerPotentialThreshold { get; set; }

        public double UpperPotentialThreshold { get; set; }

        public double PotentialLossPerTick { get; set; }

        public static NeuralNetworkConfigBuilder Builder(int seed)
        {
            return new NeuralNetworkConfigBuilder(seed);
        }

        public class NeuralNetworkConfigBuilder
        {
            private int _seed;

            private int _neuroTransmitterCount;

            private double _lowerPotentialThreshold;

            private double _upperPotentialThreshold;

            private double _potentialLossPerTick;

            public NeuralNetworkConfigBuilder(int seed)
            {
                _seed = seed;
            }

            public NeuralNetworkConfigBuilder NeuroTransmitterCount(int neuroTransmitterCount)
            {
                _neuroTransmitterCount = neuroTransmitterCount;
                return this;
            }

            public NeuralNetworkConfigBuilder LowerPotentialThreshold(double lowerPotentialThreshold)
            {
                _lowerPotentialThreshold = lowerPotentialThreshold;
                return this;
            }

            public NeuralNetworkConfigBuilder UpperPotentialThreshold(double upperPotentialThreshold)
            {
                _upperPotentialThreshold = upperPotentialThreshold;
                return this;
            }

            public NeuralNetworkConfigBuilder PotentialLossPerTick(double potentialLossPerTick)
            {
                _potentialLossPerTick = potentialLossPerTick;
                return this;
            }

            public NeuralNetworkConfig Build()
            {
                var config = new NeuralNetworkConfig(_seed)
                {
                    NeuroTransmitterCount = _neuroTransmitterCount,
                    LowerPotentialThreshold = _lowerPotentialThreshold,
                    UpperPotentialThreshold = _upperPotentialThreshold,
                    PotentialLossPerTick = _potentialLossPerTick
                };

                return config;
            }
        }
    }
}
