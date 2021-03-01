using System;
using System.Collections.Generic;

namespace AONN.NN.Neurons
{
    public abstract class AbstractNeuron : INeuron
    {
        protected AbstractNeuron(string id, NeuralNetworkConfig config)
        {
            Id = id;
            Config = config;
        }

        public readonly NeuralNetworkConfig Config;

        public IList<Synapse> Synapses { get; set; }

        public double Potential { get; set; }

        public string Id { get; protected set; }

        private double _tempPotential = 0;

        public abstract void Tick();

        public virtual void ReceivePotential(double potential)
        {
            _tempPotential += potential;
        }

        public void PostTick()
        {
            Potential += _tempPotential;
            _tempPotential = 0;
            LoosePotential();
        }

        protected virtual bool ShouldFire()
        {
            var likeliness = GetImpulseLikeliness();
            var r = Config.Rand.NextDouble();

            return r >= 1 - likeliness;
        }

        protected virtual void LoosePotential()
        {
            Potential *= Config.PotentialLossPerTick;
        }

        protected virtual void ReleaseNeuroTransmitters()
        {
            foreach (var synapse in Synapses)
            {
                synapse.Transmit();
            }
        }

        protected virtual void ResetPotential()
        {
            Potential = 0;
        }

        private double GetImpulseLikeliness()
        {
            if (Potential <= Config.LowerPotentialThreshold)
            {
                return 0;
            }
            else if (Potential > Config.UpperPotentialThreshold)
            {
                return 1;
            }
            else
            {
                var potDiff = Potential - Config.LowerPotentialThreshold;
                var thresDiff = Config.UpperPotentialThreshold - Config.LowerPotentialThreshold;
                return potDiff / thresDiff;
            }
        }

        protected virtual void Fire()
        {
            ResetPotential();
            ReleaseNeuroTransmitters();
        }

    }
}
