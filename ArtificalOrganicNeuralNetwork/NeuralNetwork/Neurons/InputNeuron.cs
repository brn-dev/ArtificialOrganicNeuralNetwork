using AONN.NN.Configs;
using System;

namespace AONN.NN.Neurons
{
    public class InputNeuron : AbstractNeuron
    {
        public InputNeuron(string id, NeuralNetworkConfig config, Action<InputNeuron> beforeTick = null) : base(id, config)
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
