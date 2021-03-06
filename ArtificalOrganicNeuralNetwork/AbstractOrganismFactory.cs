using AONN.NN;
using AONN.NN.Configs;
using AONN.NN.Neurons;

namespace AONN
{

    public abstract class AbstractOrganismFactory<T>
        where T : AbstractOrganism, new()
    {

        public delegate InputNeuron[] InputNeuronInitializer(T organism, NeuralNetworkConfig config);

        public delegate OutputNeuron[] OutputNeuronInitializer(T organism, NeuralNetworkConfig config);

        protected static T CreateOrganism(
            NeuralNetworkCreationConfig config,
            InputNeuronInitializer inputNeuronInitializer,
            OutputNeuronInitializer outputNeuronsInitializer
            )
        {
            var organism = new T();

            config.InputNeurons = inputNeuronInitializer(organism, config);

            config.OutputNeurons = outputNeuronsInitializer(organism, config);

            organism.NeuralNetwork = NeuralNetworkFactory.CreateNeuralNetwork(config);

            return organism;
        }
    }
}
