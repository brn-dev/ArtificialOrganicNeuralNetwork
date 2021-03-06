namespace AONN.NN.Neurons
{
    public interface IReceivingNeuron : INeuron
    {
        void ReceivePotential(double potential);
    }
}
