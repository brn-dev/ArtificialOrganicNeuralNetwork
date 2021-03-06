using AONN.NN.Configs;
using System;

namespace AONN.NN.Neurons
{
    public class ComputingNeuron : AbstractNeuron, IReceivingNeuron
    {

        public ComputingNeuron(string id, NeuralNetworkConfig config) : base(id, config)
        {
        }
    }
}
