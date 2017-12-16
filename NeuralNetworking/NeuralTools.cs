using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace emNeuralNet
{
	public static class NeuralTools
	{
		public static bool Save(this NeuralNetwork nn, string path = "")
		{
			//IL_000f: Unknown result type (might be due to invalid IL)
			//IL_0014: Unknown result type (might be due to invalid IL)
			//IL_0015: Unknown result type (might be due to invalid IL)
			//IL_0026: Expected Ref, but got Unknown
			//IL_002b: Unknown result type (might be due to invalid IL)
			//IL_0031: Expected Ref, but got Unknown
			//IL_0036: Unknown result type (might be due to invalid IL)
			//IL_0037: Expected Ref, but got Unknown
			//IL_0037: Unknown result type (might be due to invalid IL)
			//IL_003f: Unknown result type (might be due to invalid IL)
			//IL_0040: Expected Ref, but got Unknown
			//IL_0047: Unknown result type (might be due to invalid IL)
			//IL_004c: Unknown result type (might be due to invalid IL)
			//IL_0058: Unknown result type (might be due to invalid IL)
			//IL_005f: Unknown result type (might be due to invalid IL)
			//IL_0060: Expected Ref, but got Unknown
			//IL_0067: Unknown result type (might be due to invalid IL)
			//IL_006a: Unknown result type (might be due to invalid IL)
			//IL_006b: Expected O, but got Unknown
			try
			{
				string text = path;

				StreamWriter val2 = new StreamWriter(text);
				try
				{
					new XmlSerializer(nn.GetType()).Serialize((TextWriter)val2, nn);
					((TextWriter)val2).Flush();
				}
				finally
				{
					if (val2 != null)
					{
						((IDisposable)val2).Dispose();
					}
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static NeuralNetwork Load(this NeuralNetwork nn, string pathFile)
		{
			try
			{
				using (FileStream stream = System.IO.File.OpenRead(pathFile))
				{
					return new XmlSerializer(typeof(NeuralNetwork)).Deserialize(stream) as NeuralNetwork;
				}
			}
			catch
			{
				return null;
			}
		}

		public static Image GetImage(this NeuralNetwork nn)
		{
			//IL_003d: Unknown result type (might be due to invalid IL)
			//IL_0042: Unknown result type (might be due to invalid IL)
			//IL_0044: Unknown result type (might be due to invalid IL)
			//IL_0045: Unknown result type (might be due to invalid IL)
			//IL_004a: Unknown result type (might be due to invalid IL)
			//IL_0050: Unknown result type (might be due to invalid IL)
			//IL_0055: Unknown result type (might be due to invalid IL)
			//IL_0063: Expected Ref, but got Unknown
			//IL_00a0: Unknown result type (might be due to invalid IL)
			int width = 1600;
			int num = 1200;
			int neuronSize = 30;
			int spacingX = 45;
			int spacingY = 120;
			Bitmap val = new Bitmap(width, num);
			Graphics g = Graphics.FromImage((Image)val);
			((Graphics)g).FillRectangle(Brushes.White, 0, 0, width, num);
			int _x = 0;
			int _y = num - spacingY;
			int lidx = 0;
			nn.Layers.ForEach(delegate(Layer l)
			{
				_x = width / 2 - l.NeuronCount * spacingX / 2;
				l.Neurons.ForEach(delegate(Neuron n)
				{
					//IL_0001: Unknown result type (might be due to invalid IL)
					//IL_0006: Unknown result type (might be due to invalid IL)
					//IL_0023: Expected Ref, but got Unknown
					((Graphics)g).DrawEllipse(Pens.Black, _x, _y, neuronSize, neuronSize);
					n.Dendrites.ForEach(delegate
					{
						//IL_0035: Unknown result type (might be due to invalid IL)
						//IL_003a: Unknown result type (might be due to invalid IL)
						//IL_0072: Expected Ref, but got Unknown
						int num3 = width / 2 - nn.Layers[lidx - 1].NeuronCount * spacingX / 2;
						for (int i = 0; i < nn.Layers[lidx - 1].NeuronCount; i++)
						{
							((Graphics)g).DrawLine(Pens.LightGray, _x + neuronSize / 2, _y + neuronSize, num3 + neuronSize / 2, _y + spacingY);
							num3 += spacingX;
						}
					});
					_x += spacingX;
				});
				_y -= spacingY;
				int num2 = ++lidx;
			});
			return (Image)val;
		}

		public static void SaveImage(this NeuralNetwork nn, string pathFile)
		{
			//IL_0001: Unknown result type (might be due to invalid IL)
			//IL_0007: Expected Ref, but got Unknown
			((Image)nn.GetImage()).Save(pathFile);
		}

		public static int BinaryToInt(this NeuralData nd)
		{
			return NeuralData.BinaryToInt(nd.ToArray());
		}

		public static NeuralData InputDataInt(this NeuralNetwork nw, long number)
		{
			return new NeuralData(NeuralData.ConvertToBinary(number, nw.InputNeuronsCount));
		}

		public static NeuralData OutputDataInt(this NeuralNetwork nw, long number)
		{
			return new NeuralData(NeuralData.ConvertToBinary(number, nw.OutputNeuronsCount));
		}
	}
}
