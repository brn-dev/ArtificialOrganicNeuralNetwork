using AONN.NeuralNetwork;
using AONN.NeuralNetwork.Neurons;

namespace AONN
{
    public class OrganismFactory
    {

        public static Organism CreateOrganism()
        {
            var organism = new Organism(100);

            var config = new NeuralNetworkCreationConfig(42069)
            {
                ComputingNeuronCount = 1,
                SynapseCountMean = 1,
                SynapseCountStdDev = 0,
                NeuroTransmitterCount = 1,
            };

            config.InputNeurons = new InputNeuron[]
            {
                new BooleanConstantInputNeuron(config, 10, () => config.Rand.NextDouble() >= 0.5), 
            };

            config.OutputNeurons = new OutputNeuron[]
            {
                new OutputNeuron(config, () => organism.Position.X += 1), 
            };

            organism.NeuralNetwork = NeuralNetworkFactory.CreateNeuralNetwork(config); ;

            return organism;
        }
    }
}
