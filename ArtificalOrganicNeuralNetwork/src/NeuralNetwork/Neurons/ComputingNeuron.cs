using System;

namespace AONN.NN.Neurons
{
    public class ComputingNeuron : AbstractNeuron, IReceivingNeuron
    {

        public ComputingNeuron(string id, NeuralNetworkConfig config) : base(id, config)
        {
        }

        public override void Tick()
        {
            if (ShouldFire())
            {
                Fire();
            }
        }
    }
}
