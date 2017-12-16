using System;
using System.Collections.Generic;
using System.Text;

namespace emNeuralNet
{
	[Serializable]
	public class NeuralNetwork
	{
		public string Name
		{
			get;
			set;
		}

		public List<Layer> Layers
		{
			get;
			set;
		}

		public double LearningRate
		{
			get;
			set;
		}

		public ActivationFunctions.Activation Activation
		{
			get;
			set;
		}

		public long TrainingRounds
		{
			get;
			set;
		}

		public int InputNeuronsCount
		{
			get
			{
				return this.Layers[0].NeuronCount;
			}
		}

		public int OutputNeuronsCount
		{
			get
			{
				return this.Layers[this.LayerCount - 1].NeuronCount;
			}
		}

		public int LayerCount
		{
			get
			{
				return this.Layers.Count;
			}
		}

		public NeuralNetwork()
		{
		}

		public NeuralNetwork(double learningRate, int[] layers, ActivationFunctions.Activation activation = ActivationFunctions.Activation.SIGMOID, string name = "")
		{
			if (layers.Length >= 2)
			{
				this.LearningRate = learningRate;
				this.Layers = new List<Layer>();
				this.Activation = activation;
				this.TrainingRounds = 0L;
				if (name != "")
				{
					this.Name = name;
				}
				else
				{
					this.Name = "NeuralNetWork-" + Guid.NewGuid().ToString();
				}
				int i;
				for (i = 0; i < layers.Length; i++)
				{
					Layer layer = new Layer(layers[i]);
					this.Layers.Add(layer);
					for (int j = 0; j < layers[i]; j++)
					{
						layer.Neurons.Add(new Neuron());
					}
					layer.Neurons.ForEach(delegate(Neuron nn)
					{
						if (i == 0)
						{
							nn.Bias = 0.0;
						}
						else
						{
							for (int k = 0; k < layers[i - 1]; k++)
							{
								nn.Dendrites.Add(new Dendrite());
							}
						}
					});
				}
			}
		}

		public NeuralData Run(NeuralData input)
		{
			if (input.Count != this.Layers[0].NeuronCount)
			{
				return null;
			}
			for (int i = 0; i < this.Layers.Count; i++)
			{
				Layer layer = this.Layers[i];
				for (int j = 0; j < layer.Neurons.Count; j++)
				{
					Neuron neuron = layer.Neurons[j];
					if (i == 0)
					{
						neuron.Value = ((List<double>)input)[j];
					}
					else
					{
						neuron.Value = 0.0;
						for (int k = 0; k < this.Layers[i - 1].Neurons.Count; k++)
						{
							neuron.Value += this.Layers[i - 1].Neurons[k].Value * neuron.Dendrites[k].Weight;
						}
						neuron.Value = ActivationFunctions.Activate(this.Activation, neuron.Value + neuron.Bias);
					}
				}
			}
			Layer layer2 = this.Layers[this.Layers.Count - 1];
			double[] array = new double[layer2.Neurons.Count];
			for (int l = 0; l < layer2.Neurons.Count; l++)
			{
				array[l] = layer2.Neurons[l].Value;
			}
			return new NeuralData(array);
		}

		public bool Train(NeuralData input, NeuralData output, long rounds = 1L)
		{
			if (input.Count == this.Layers[0].Neurons.Count && output.Count == this.Layers[this.Layers.Count - 1].Neurons.Count)
			{
				for (long num = 0L; num < rounds; num++)
				{
					this.Run(input);
					for (int i = 0; i < this.Layers[this.Layers.Count - 1].Neurons.Count; i++)
					{
						Neuron neuron = this.Layers[this.Layers.Count - 1].Neurons[i];
						neuron.Delta = neuron.Value * (1.0 - neuron.Value) * (((List<double>)output)[i] - neuron.Value);
						for (int num2 = this.Layers.Count - 2; num2 >= 1; num2--)
						{
							for (int j = 0; j < this.Layers[num2].Neurons.Count; j++)
							{
								Neuron neuron2 = this.Layers[num2].Neurons[j];
								neuron2.Delta = neuron2.Value * (1.0 - neuron2.Value) * this.Layers[num2 + 1].Neurons[i].Dendrites[j].Weight * this.Layers[num2 + 1].Neurons[i].Delta;
							}
						}
					}
					for (int num3 = this.Layers.Count - 1; num3 >= 1; num3--)
					{
						for (int k = 0; k < this.Layers[num3].Neurons.Count; k++)
						{
							Neuron neuron3 = this.Layers[num3].Neurons[k];
							neuron3.Bias += this.LearningRate * neuron3.Delta;
							for (int l = 0; l < neuron3.Dendrites.Count; l++)
							{
								neuron3.Dendrites[l].Weight = neuron3.Dendrites[l].Weight + this.LearningRate * this.Layers[num3 - 1].Neurons[l].Value * neuron3.Delta;
							}
						}
					}
					long num4 = this.TrainingRounds += 1L;
				}
				return true;
			}
			return false;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("Name: " + this.Name);
			stringBuilder.AppendLine();
			for (int i = 0; i < this.Layers.Count; i++)
			{
				StringBuilder stringBuilder2 = stringBuilder;
				string[] obj = new string[5]
				{
					"\tLayer ",
					i.ToString(),
					" (",
					null,
					null
				};
				int num = this.Layers[i].NeuronCount;
				obj[3] = num.ToString();
				obj[4] = " neurons)";
				stringBuilder2.AppendLine(string.Concat(obj));
				for (int j = 0; j < this.Layers[i].NeuronCount; j++)
				{
					StringBuilder stringBuilder3 = stringBuilder;
					string[] obj2 = new string[5]
					{
						"\t\tNeuron ",
						j.ToString(),
						" (",
						null,
						null
					};
					num = this.Layers[i].Neurons[j].DendriteCount;
					obj2[3] = num.ToString();
					obj2[4] = " dendrites)";
					stringBuilder3.AppendLine(string.Concat(obj2));
					StringBuilder stringBuilder4 = stringBuilder;
					double num2 = this.Layers[i].Neurons[j].Bias;
					stringBuilder4.AppendLine("\t\t\tBias: " + num2.ToString());
					StringBuilder stringBuilder5 = stringBuilder;
					num2 = this.Layers[i].Neurons[j].Delta;
					stringBuilder5.AppendLine("\t\t\tDelta: " + num2.ToString());
					StringBuilder stringBuilder6 = stringBuilder;
					num2 = this.Layers[i].Neurons[j].Value;
					stringBuilder6.AppendLine("\t\t\tValue: " + num2.ToString());
					stringBuilder.AppendLine("\t\t\tDendrites");
					for (int k = 0; k < this.Layers[i].Neurons[j].DendriteCount; k++)
					{
						stringBuilder.AppendLine("\t\t\t\tDendrite " + k.ToString() + " weight: " + this.Layers[i].Neurons[j].Dendrites[k].Weight);
					}
				}
			}
			return stringBuilder.ToString();
		}
	}
}
