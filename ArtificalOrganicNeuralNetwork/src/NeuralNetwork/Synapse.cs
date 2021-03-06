using AONN.NN.Neurons;
using System.Collections.Generic;

namespace AONN.NN
{
    public class NeuroTransmitterAffinity
    {
        public NeuroTransmitter Transmitter { get; set; }

        public double Affinity { get; set; }

        public NeuroTransmitterAffinity(NeuroTransmitter transmitter, double affinity)
        {
            Transmitter = transmitter;
            Affinity = affinity;
        }
    }

    public class Synapse
    {
        public double Strength { get; set; }

        public INeuron SourceNeuron { get; set; }

        public IReceivingNeuron TargetNeuron { get; set; }

        public NeuroTransmitterAffinity[] TransmitterAffinities { get; set; }

        public NeuroTransmitterSet NeuroTransmitterSet { get; set; }

        public Synapse(
            double strength, 
            INeuron sourceNeuron, 
            IReceivingNeuron targetNeuron, 
            NeuroTransmitterAffinity[] transmitterAffinities,
            NeuroTransmitterSet neuroTransmitterSet
        ) {
            Strength = strength;
            SourceNeuron = sourceNeuron;
            TargetNeuron = targetNeuron;
            TransmitterAffinities = transmitterAffinities;
            NeuroTransmitterSet = neuroTransmitterSet;
        }

        public void Transmit()
        {
            var totalPotential = 0.0;
            for (int i = 0; i < TransmitterAffinities.Length; i++)
            {
                totalPotential += Strength * TransmitterAffinities[i].Affinity;
            }
            TargetNeuron.ReceivePotential(totalPotential);
        }
    }
}
