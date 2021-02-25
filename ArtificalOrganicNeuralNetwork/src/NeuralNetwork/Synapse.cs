using AONN.NeuralNetwork.Neurons;
using System.Collections.Generic;

namespace AONN.NeuralNetwork
{
    public class Synapse
    {
        public double Strength { get; set; }

        public INeuron SourceNeuron { get; set; }

        public IReceivingNeuron TargetNeuron { get; set; }

        public IDictionary<NeuroTransmitter, double> SourceAffinities { get; set; }

        public IDictionary<NeuroTransmitter, double> TargetAffinities { get; set; }

        public NeuroTransmitterSet NeuroTransmitterSet { get; set; }

        public Synapse(
            double strength, 
            INeuron sourceNeuron, 
            IReceivingNeuron targetNeuron, 
            IDictionary<NeuroTransmitter, double> sourceAffinities, 
            IDictionary<NeuroTransmitter, double> targetAffinities, 
            NeuroTransmitterSet neuroTransmitterSet
        ) {
            Strength = strength;
            SourceNeuron = sourceNeuron;
            TargetNeuron = targetNeuron;
            SourceAffinities = sourceAffinities;
            TargetAffinities = targetAffinities;
            NeuroTransmitterSet = neuroTransmitterSet;
        }

        public void Transmit()
        {
            var totalPotential = 0.0;
            foreach (var transmitter in NeuroTransmitterSet.Transmitters)
            {
                totalPotential += Strength * SourceAffinities[transmitter] * TargetAffinities[transmitter];
            }
            TargetNeuron.ReceivePotential(totalPotential);
        }
    }
}
