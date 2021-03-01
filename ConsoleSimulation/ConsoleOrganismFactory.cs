using AONN;
using AONN.NN;

namespace ConsoleSimulation
{
    public class ConsoleOrganismFactory : AbstractOrganismFactory<ConsoleOrganism>
    {
        public static ConsoleOrganism CreateConsoleOrgansim(
            NeuralNetworkCreationConfig config,
            InputNeuronInitializer inputNeuronInitializer,
            OutputNeuronsInitializer outputNeuronsInitializer
            )
        {
            var organism = CreateOrganism(config, inputNeuronInitializer, outputNeuronsInitializer);

            return organism;
        }
    }
}
