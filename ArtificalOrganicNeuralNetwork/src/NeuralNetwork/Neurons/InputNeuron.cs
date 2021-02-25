using System;

namespace AONN.NeuralNetwork.Neurons
{
    public class InputNeuron : AbstractNeuron
    {
        public InputNeuron(NeuralNetworkConfig config, Action<InputNeuron> beforeTick = null) : base(config)
        {
            BeforeTick = beforeTick;
        }
        private Action<InputNeuron> BeforeTick { get; }

        public override void Tick()
        {
            BeforeTick?.Invoke(this);

            if (ShouldFire())
            {
                Fire();
            }
        }

        
    }
}
