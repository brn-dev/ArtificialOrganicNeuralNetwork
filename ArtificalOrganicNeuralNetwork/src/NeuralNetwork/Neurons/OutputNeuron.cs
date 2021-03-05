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

        public static OutputNeuron[] Times(
            int times,
            string baseId, 
            NeuralNetworkConfig config, 
            Action onFire
            )
        {
            var neurons = new OutputNeuron[times];

            for (int i = 0; i < times; i++)
            {
                neurons[i] = new OutputNeuron($"{baseId}/{i}", config, onFire);
            }

            return neurons;
        }
    }
}
