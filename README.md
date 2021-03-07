# Solution Structure

## ArtificialOrganicNeuralNetwork
The neural network itself

## ConsoleSimulation
A simulation for the neural network where it can roam around a 2D world with discrete coordinates. 
The console window is used for rendering.



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

### Strength
Each synapse has a certain strength. This strength influences how much potential gets passed ot the receiving neuron. 
Whenever the synapse releases neuro-transmitters, the strength increases by a small amount. 
Every couple ticks, the strength decreases automatically.  
Strength is analogous to the [Myelin](https://en.wikipedia.org/wiki/Myelin) in actual neurons. 
However, an important difference is that in the AONN, more strength/myelin doesn't cause the signal to travel faster to the receiving neuron, like it would in actual neurons.

### Neuro-Transmitters
A neuron and its synapses can have 1..n transmitters that it uses to transmit potential to the connected neurons.
These transmitters are currently doing nothing more than being a weight which influences the potential passed to the receiving neuron. 
The total potential which gets passed to the receiving neurons is calculated by taking the sum of the products of strength times the synapse-affinity of each neuro-transmitter.  
  
neuro-transmitters: array of the neuro-transmitters used in the synapse
neuro-transmitter-affinities: array of doubles, which represent how strong each neuro-transmitter is represented in the synapse.  
PotentialPassedToNextNeuron = Sum(i: 0 -> len(neuro-transmitters), SynapseStrength * neuro-transmitter-affinities[i])