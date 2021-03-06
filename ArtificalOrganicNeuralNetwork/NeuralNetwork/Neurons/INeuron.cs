using System.Collections.Generic;

namespace AONN.NN.Neurons
{
    public interface INeuron
    {
        void Tick();
        void PostTick();
        void SuperTick();
        IList<Synapse> Synapses { get; set; }
        NeuroTransmitterSet NeuroTransmitterSet { get; set; }

        string Id { get; }
    }
}
