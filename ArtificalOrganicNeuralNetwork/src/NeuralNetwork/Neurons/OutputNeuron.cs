using System;

namespace AONN.NN.Neurons
{
    public class OutputNeuron : AbstractNeuron, IReceivingNeuron
    {
        public OutputNeuron(string id, NeuralNetworkConfig config, Action onFire) : base(id, config)
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

        protected override void ReleaseNeuroTransmitters()
        {
            OnFire();
        }
    }
}
