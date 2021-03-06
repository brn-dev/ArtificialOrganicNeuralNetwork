# Artificial Organic Neural Network

## Neurons
Neurons are the nodes of the network. 
They hold a potential and if that potential exceeds a certain threshold, the neuron fires. 
This causes to the neurons synapses to release neuro-transmitters which add potential to the receiving neuron.

### Input Neurons
Input neurons are used to get new information into the network. Basically the senses for the neural network.

### Output Neurons
Output neurons are used to let the network interact with its surroundings, e.g. move around.

### Computing Neurons
Connect input- and output-neurons and (hopefully) make the neural network "think".

## Synapses
The edges of the network.
They connect neurons and basically pass potential from one neuron to the next.
Each synapse has a certain strength. This strength influences how much potential gets passed ot the receiving neuron. 
Whenever the synapse releases neuro-transmitters, the strength increases by a small amount. 
Every couple ticks, the strength decreases automatically.

## Neuro-Transmitters
A neuron and its synapses can have 1..n transmitters that it uses to transmit potential to the connected neurons.
These transmitters are currently nothing more than a weight, so the total potential that will be passed to the receiving neuron is
sum(SynapseStrength * NeuroTransmitterStrength[i]) 

