using AONN.NN.Configs;
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

        protected override void ReleaseNeuroTransmitters()
        {
            OnFire();
        }

        public override void SuperTick()
        {
            LoosePotential();
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
