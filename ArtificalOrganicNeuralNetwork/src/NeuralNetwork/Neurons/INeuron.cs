using System.Collections.Generic;

namespace AONN.NN.Neurons
{
    public interface INeuron
    {
        void Tick();
        void PostTick();
        IList<Synapse> Synapses { get; set; }

        string Id { get; }
    }
}
