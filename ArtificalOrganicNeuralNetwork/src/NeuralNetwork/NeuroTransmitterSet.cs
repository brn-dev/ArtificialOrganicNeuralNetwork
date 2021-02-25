using System;
using System.Linq;

namespace AONN.NeuralNetwork
{
    public class NeuroTransmitterSet
    {

        private readonly int _transmitterCount;
        private readonly Random _rand;

        public NeuroTransmitterSet(Random rand, int transmitterCount)
        {
            _rand = rand;
            _transmitterCount = transmitterCount;
            GenerateTransmitters();
        }

        public NeuroTransmitter[] Transmitters { get; private set; }

        public NeuroTransmitter GetTransmitter(int id)
        {
            return Transmitters.FirstOrDefault(t => t.Id == id);
        }

        private void GenerateTransmitters()
        {
            Transmitters = new NeuroTransmitter[_transmitterCount];
            for (int i = 0; i < _transmitterCount; i++)
            {
                Transmitters[i] = new NeuroTransmitter(i);
            }
        }
    }
}
