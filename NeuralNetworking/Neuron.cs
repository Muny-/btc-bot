using System.Collections.Generic;

namespace emNeuralNet
{
	public class Neuron
	{
		public List<Dendrite> Dendrites
		{
			get;
			set;
		}

		public double Bias
		{
			get;
			set;
		}

		public double Delta
		{
			get;
			set;
		}

		public double Value
		{
			get;
			set;
		}

		public int DendriteCount
		{
			get
			{
				return this.Dendrites.Count;
			}
		}

		public Neuron()
		{
			RandomGenerator randomGenerator = new RandomGenerator();
			this.Bias = randomGenerator.RandomValue;
			this.Dendrites = new List<Dendrite>();
		}
	}
}
