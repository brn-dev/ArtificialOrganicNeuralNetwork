using System.Collections.Generic;

namespace AONN.NeuralNetwork.Neurons
{
    public interface INeuron
    {
        void Tick();
        void PostTick();
        IList<Synapse> Synapses { get; set; }
    }
}
