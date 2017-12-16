using System;
using System.Collections.Generic;

namespace emNeuralNet
{
	public class NeuralData : List<double>
	{
		public enum DATATYPE
		{
			INPUT,
			OUTPUT
		}

		public NeuralData()
		{
		}

		public NeuralData(params double[] data)
		{
			base.AddRange(data);
		}

		public static double[] ConvertToBinary(long number, int neuronCount)
		{
			return NeuralData.StringToNeural(Convert.ToString(number, 2).PadLeft(neuronCount, '0'));
		}

		public static double[] ConvertToBinary(char character, int neuronCount)
		{
			return NeuralData.StringToNeural(Convert.ToString(character, 2).PadLeft(neuronCount, '0'));
		}

		public static int BinaryToInt(double[] input)
		{
			string text = "";
			foreach (double a in input)
			{
				text += Math.Round(a);
			}
			return Convert.ToInt32(text, 2);
		}

		private static double[] StringToNeural(string input)
		{
			NeuralData neuralData = new NeuralData();
			char[] array = input.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				char c = array[i];
				double item = default(double);
				double.TryParse(c.ToString(), out item);
				neuralData.Add(item);
			}
			return neuralData.ToArray();
		}
	}
}
