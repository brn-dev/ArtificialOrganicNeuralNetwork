using System;

namespace AONN.NeuralNetwork
{
    public class NeuroTransmitter : IEquatable<NeuroTransmitter>
    {
    
        public NeuroTransmitter(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public bool Equals(NeuroTransmitter other)
        {
            return other != null && Id == other.Id;
        }
    }
}
