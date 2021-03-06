using AONN.NN.Configs;

namespace AONN.NN.Neurons
{
    public delegate bool Predicate();

    public class BooleanConstantInputNeuron : InputNeuron
    {

        public BooleanConstantInputNeuron(string id, NeuralNetworkConfig config, double potentialPerTick, Predicate shouldReceivePotential) :
            base(
                id,
                config, 
                (_base) => { 
                    if (shouldReceivePotential()) 
                    {
                        _base.ReceivePotential(potentialPerTick);
                    } 
                }
            )
        {
        }

    }
}
