namespace AONN.NeuralNetwork.Neurons
{
    public delegate bool Predicate();

    public class BooleanConstantInputNeuron : InputNeuron
    {

        public BooleanConstantInputNeuron(NeuralNetworkConfig config, double potentialPerTick, Predicate shouldReceivePotential) :
            base(
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
