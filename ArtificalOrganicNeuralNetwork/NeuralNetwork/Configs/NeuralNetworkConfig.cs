using Newtonsoft.Json;
using System;

namespace AONN.NN.Configs
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

        public int TicksPerSuperTick { get; set; }

        public double LowerPotentialThreshold { get; set; }

        public double UpperPotentialThreshold { get; set; }

        public double PotentialLossPerSuperTick { get; set; }

        public double StrengthGainPerTransmit { get; set; }

        public double StrengthLossPerSuperTick { get; set; }
    }
}
