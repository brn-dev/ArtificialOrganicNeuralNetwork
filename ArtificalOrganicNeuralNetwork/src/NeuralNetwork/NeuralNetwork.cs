using AONN.NN.Neurons;
using System.Collections.Generic;
using System.Linq;

namespace AONN.NN
{
    public class NeuralNetwork
    {
        public NeuralNetworkConfig Config { get; private set; }

        public INeuron[] AllNeurons { get; }

        public IEnumerable<ComputingNeuron> ComputingNeurons { get; }

        public IEnumerable<InputNeuron> InputNeurons { get; }

        public IEnumerable<OutputNeuron> OutputNeurons { get; private set; }

        public NeuralNetwork(
            NeuralNetworkConfig config,
            IEnumerable<ComputingNeuron> computingNeurons, 
            IEnumerable<InputNeuron> inputNeurons, 
            IEnumerable<OutputNeuron> outputNeurons
        ) {
            Config = config;
            ComputingNeurons = computingNeurons;
            InputNeurons = inputNeurons;
            OutputNeurons = outputNeurons;

            AllNeurons = computingNeurons.Cast<INeuron>().Concat(inputNeurons.Cast<INeuron>()).Concat(outputNeurons.Cast<INeuron>()).ToArray();
        }

        public void Tick()
        {
            for (int i = 0; i < AllNeurons.Length; i++)
            {
                AllNeurons[i].Tick();
            }
            for (int i = 0; i < AllNeurons.Length; i++)
            {
                AllNeurons[i].PostTick();
            }
        }
    }
}
