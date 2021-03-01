using AONN.NN;
using AONN.NN.Neurons;

namespace AONN
{

    public abstract class AbstractOrganismFactory<T>
        where T : AbstractOrganism, new()
    {

        public delegate InputNeuron[] InputNeuronInitializer(T organism, NeuralNetworkConfig config);

        public delegate OutputNeuron[] OutputNeuronsInitializer(T organism, NeuralNetworkConfig config);

        protected static T CreateOrganism(
            NeuralNetworkCreationConfig config,
            InputNeuronInitializer inputNeuronInitializer,
            OutputNeuronsInitializer outputNeuronsInitializer
            )
        {
            var organism = new T();

            config.InputNeurons = inputNeuronInitializer(organism, config.NeuralNetworkConfig);

            config.OutputNeurons = outputNeuronsInitializer(organism, config.NeuralNetworkConfig);

            organism.NeuralNetwork = NeuralNetworkFactory.CreateNeuralNetwork(config);

            return organism;
        }
    }
}
