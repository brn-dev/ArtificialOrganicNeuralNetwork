using System;
using System.Linq;

namespace AONN.NN
{
    public class NeuroTransmitterSet
    {
        public NeuroTransmitterSet(int transmitterCount)
        {
            GenerateTransmitters(transmitterCount);
        }

        public NeuroTransmitterSet(NeuroTransmitter[] neuroTransmitters)
        {
            Transmitters = (NeuroTransmitter[])neuroTransmitters.Clone();
        }

        public NeuroTransmitter[] Transmitters { get; private set; }

        public NeuroTransmitter GetTransmitter(int id)
        {
            return Transmitters.FirstOrDefault(t => t.Id == id);
        }

        public NeuroTransmitterSet RandomSubSet(Random rand, int size)
        {
            if (size > Transmitters.Length)
            {
                size = Transmitters.Length;
            } 
            else if (size == Transmitters.Length)
            {
                return new NeuroTransmitterSet(Transmitters);
            } 
            else if (size < 1)
            {
                throw new ArgumentException("Size must be greather than 0!");
            }

            var subSet = new NeuroTransmitter[size];

            var transmitters = Transmitters.ToList();

            for (int i = 0; i < size; i++)
            {
                var randIdx = rand.Next(0, size);

                subSet[i] = transmitters[randIdx];

                transmitters.RemoveAt(randIdx);
            }

            return new NeuroTransmitterSet(subSet);
        }

        private void GenerateTransmitters(int transmitterCount)
        {
            Transmitters = new NeuroTransmitter[transmitterCount];
            for (int i = 0; i < transmitterCount; i++)
            {
                Transmitters[i] = new NeuroTransmitter(i);
            }
        }
    }
}
