namespace emNeuralNet
{
	public class Dendrite
	{
		public double Weight
		{
			get;
			set;
		}

		public Dendrite()
		{
			RandomGenerator randomGenerator = new RandomGenerator();
			this.Weight = randomGenerator.RandomValue;
		}
	}
}
