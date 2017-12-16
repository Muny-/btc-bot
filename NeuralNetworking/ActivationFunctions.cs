using System;

namespace emNeuralNet
{
	public class ActivationFunctions
	{
		public enum Activation
		{
			SIGMOID,
			TANH,
			STEP
		}

		public static double Activate(Activation a, double input)
		{
			double result = 0.0;
			switch (a)
			{
			case Activation.SIGMOID:
				result = ActivationFunctions.Sigmoid(input);
				break;
			case Activation.TANH:
				result = ActivationFunctions.TanH(input);
				break;
			case Activation.STEP:
				result = ActivationFunctions.Step(input);
				break;
			}
			return result;
		}

		private static double Sigmoid(double x)
		{
			if (x < -45.0)
			{
				return 0.0;
			}
			if (x > 45.0)
			{
				return 1.0;
			}
			return 1.0 / (1.0 + Math.Exp(0.0 - x));
		}

		private static double TanH(double x)
		{
			if (x < -45.0)
			{
				return -1.0;
			}
			if (x > 45.0)
			{
				return 1.0;
			}
			return Math.Tanh(x);
		}

		public static double Step(double x)
		{
			if (x < 0.0)
			{
				return 0.0;
			}
			return 1.0;
		}
	}
}
