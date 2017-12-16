using System;
using System.Security.Cryptography;

namespace emNeuralNet
{
	public class RandomGenerator
	{
		public double RandomValue
		{
			get;
			set;
		}

		public RandomGenerator()
		{
			//IL_0006: Unknown result type (might be due to invalid IL)
			//IL_000b: Unknown result type (might be due to invalid IL)
			//IL_000c: Unknown result type (might be due to invalid IL)
			//IL_000d: Expected O, but got Unknown
			//IL_0026: Unknown result type (might be due to invalid IL)
			//IL_0029: Unknown result type (might be due to invalid IL)
			//IL_002a: Expected O, but got Unknown
			RNGCryptoServiceProvider val = new RNGCryptoServiceProvider();
			try
			{
				Random random = new Random(((object)val).GetHashCode());
				this.RandomValue = random.NextDouble();
			}
			finally
			{
				if (val != null)
				{
					((IDisposable)val).Dispose();
				}
			}
		}
	}
}
