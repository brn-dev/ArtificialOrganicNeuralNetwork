using AONN.NN.Configs;
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

        public NeuroTransmitterSet NeuroTransmitterSet { get; set; }

        public double Potential { get; set; }

        public string Id { get; protected set; }

        private double _tempPotential = 0;

        public virtual void Tick()
        {
            if (ShouldFire())
            {
                Fire();
            }
        }

        public virtual void SuperTick()
        {
            LoosePotential();

            var synapseCount = Synapses.Count;
            for (int i = 0; i < synapseCount; i++)
            {
                Synapses[i].SuperTick();
            }
        }

        public virtual void ReceivePotential(double potential)
        {
            _tempPotential += potential;
        }

        public void PostTick()
        {
            Potential += _tempPotential;
            _tempPotential = 0; 
        }

        protected virtual bool ShouldFire()
        {
            var likeliness = GetImpulseLikeliness();
            var r = Config.Rand.NextDouble();

            return r <= likeliness;
        }

        protected virtual void LoosePotential()
        {
            Potential *= Config.PotentialLossPerSuperTick;
        }

        protected virtual void ReleaseNeuroTransmitters()
        {
            var length = Synapses.Count;
            for (int i = 0; i < length; i++)
            {
                Synapses[i].Transmit();
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
