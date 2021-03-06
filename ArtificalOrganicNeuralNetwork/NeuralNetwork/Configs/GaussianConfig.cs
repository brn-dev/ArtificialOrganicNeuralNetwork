namespace AONN.NN.Configs
{
    public class GaussianConfig
    {
        public GaussianConfig(double mean, double stdDev, double min = double.MinValue)
        {
            Mean = mean;
            StdDev = stdDev;
            Min = min;
        }

        public double Mean { get; set; }

        public double StdDev { get; set; }

        public double Min { get; set; }
    }
}
