using System.Collections.Generic;

namespace emNeuralNet
{
	public class Layer
	{
		public List<Neuron> Neurons
		{
			get;
			set;
		}

		public int NeuronCount
		{
			get
			{
				return this.Neurons.Count;
			}
		}

		public Layer()
		{
		}

		public Layer(int numNeurons)
		{
			this.Neurons = new List<Neuron>(numNeurons);
		}
	}
}
