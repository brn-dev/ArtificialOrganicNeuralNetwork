using AONN.NN;
using AONN.NN.Configs;

namespace AONN
{
    public abstract class AbstractOrganism
    {
        public NeuralNetwork NeuralNetwork { get; set; }

        public NeuralNetworkConfig Config => NeuralNetwork.Config;

        public virtual void Tick(long tick)
        {
            NeuralNetwork.Tick(tick);
        }
    }
}
