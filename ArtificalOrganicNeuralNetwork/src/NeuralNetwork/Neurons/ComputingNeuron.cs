using System;

namespace AONN.NeuralNetwork.Neurons
{
    public class ComputingNeuron : AbstractNeuron, IReceivingNeuron
    {

        public ComputingNeuron(NeuralNetworkConfig config) : base(config)
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
