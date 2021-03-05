using AONN;
using AONN.NN;

namespace ConsoleSimulation
{
    public class ConsoleOrganismFactory : AbstractOrganismFactory<ConsoleOrganism>
    {
        public static ConsoleOrganism CreateConsoleOrgansim(
            World world,
            NeuralNetworkCreationConfig config,
            InputNeuronInitializer inputNeuronInitializer,
            OutputNeuronInitializer outputNeuronsInitializer
            )
        {
            var organism = CreateOrganism(config, inputNeuronInitializer, outputNeuronsInitializer);

            organism.World = world;
            organism.FoodCollected = 0;

            return organism;
        }
    }
}
