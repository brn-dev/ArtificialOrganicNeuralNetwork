﻿using System;
using System.IO;
using AONN.NN.Neurons;
using Newtonsoft.Json;

namespace AONN.NN
{
    public class NeuralNetworkCreationConfig : NeuralNetworkConfig
    {
        public NeuralNetworkCreationConfig(int creationSeed, int seed) : base(seed)
        {
            CreationSeed = creationSeed;
            CreationRand = new Random(creationSeed);
        }

        public int CreationSeed { get; }

        [JsonIgnore]
        public Random CreationRand { get; }

        public int ComputingNeuronCount { get; set; }

        [JsonIgnore]
        public InputNeuron[] InputNeurons { get; set; }

        [JsonIgnore]
        public OutputNeuron[] OutputNeurons { get; set; }

        public double SynapseCountMean { get; set; }

        public double SynapseCountStdDev { get; set; }

        public double SynapseStrengthMean { get; set; }

        public double SynapseStrengthStdDev { get; set; }

        public string ToJson()
        {
            var serializer = JsonSerializer.CreateDefault(new JsonSerializerSettings { Formatting = Formatting.Indented });
            var stringWriter = new StringWriter();
            serializer.Serialize(stringWriter, this);
            return stringWriter.ToString();
        }

        public override string ToString()
        {
            return ToJson();
        }

    }
}
