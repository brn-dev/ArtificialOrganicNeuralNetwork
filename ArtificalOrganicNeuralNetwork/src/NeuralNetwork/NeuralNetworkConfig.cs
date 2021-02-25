using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AONN.NeuralNetwork
{
    public class NeuralNetworkConfig
    {
        public NeuralNetworkConfig(int seed, Random rand)
        {
            Seed = seed;
            Rand = rand;
        }

        public int Seed { get; private set; }

        public Random Rand { get; private set; }

        public int NeuroTransmitterCount { get; set; }

        public double LowerPotentialThreshold { get; } = 100.0;

        public double UpperPotentialThreshold { get; } = 120;

        public double PotentialLossPerTick { get; } = 0.95;
    }
}
