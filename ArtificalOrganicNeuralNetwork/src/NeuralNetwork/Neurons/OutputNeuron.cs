using System;

namespace AONN.NeuralNetwork.Neurons
{
    public class OutputNeuron : AbstractNeuron, IReceivingNeuron
    {
        public OutputNeuron(NeuralNetworkConfig config, Action onFire) : base(config)
        {
            OnFire = onFire;
        }

        public Action OnFire { get; set; }

        public override void Tick()
        {
            if (ShouldFire())
            {
                Fire();
            }
        }

        protected override void Fire()
        {
            OnFire();
            ReleaseNeuroTransmitters();
            ResetPotential();
        }
    }
}
