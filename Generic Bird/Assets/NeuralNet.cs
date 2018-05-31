using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets
{
    public class NeuralNet
    {
        public List<float> inputData;
        float output;
        public List<List<List<float>>> Layers;
        static public int layersCount = 3,
            neuronCount = 10;
        private System.Random rand;
        public float fitness = 0;
        public NeuralNet(List<List<List<float>>> Lists, int seed)
        {
            rand = new System.Random(seed);
            Layers = new List<List<List<float>>>(Lists);
        }
        public NeuralNet(int seed)
        {
            rand = new System.Random(seed);
            List<float> nn = new List<float>(new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

            List<List<float>> ii = new List<List<float>>();
            List<List<List<float>>> xx = new List<List<List<float>>>();
            
            for (int i = 0; i < layersCount; i++)
            {
                for (int n = 0; n < neuronCount; n++)
                {
                    ii.Add(new List<float>(new float[] 
                    { 
                    (float)rand.NextDouble()*2-1,
                    (float)rand.NextDouble()*2-1,
                    (float)rand.NextDouble()*2-1,
                    (float)rand.NextDouble()*2-1,
                    (float)rand.NextDouble()*2-1,
                    (float)rand.NextDouble()*2-1,
                    (float)rand.NextDouble()*2-1,
                    (float)rand.NextDouble()*2-1,
                    (float)rand.NextDouble()*2-1,
                    (float)rand.NextDouble()*2-1

                    }));
                }
                xx.Add(new List<List<float>>(ii));
                ii = new List<List<float>>();
            }
            Layers = new List<List<List<float>>>(xx);
        }
        float activation(float x) => (x<0) ? -1 : 1;
        //return (float)(1 / (1 + Math.Exp(-x))) * 2 - 1;
        
        public int mutate(int index)
        {
            int LayersCount = Layers.Count(),
                NeuronCount = Layers[0].Count(),
                NeuronsCapacity = Layers[0][0].Count(),
                r2 = rand.Next(0, NeuronCount),
                r3 = rand.Next(0, NeuronsCapacity);
                Layers[index][r2][r3] = (float)rand.NextDouble()*2-1;
            return 0;
        }
        public static List<List<float>> crossLayers(List<List<float>> Layer1, List<List<float>> Layer2,int mid,int ver)
        {
            List<List<float>> Layer = new List<List<float>>();
            List<float> neuron = new List<float>();
            int len = Math.Min(Layer2.Count, Layer1.Count);
            for (int i = 0; i < len; i++)
            {

                if (ver == 1)
                {

                    if (mid < i)
                        Layer.Add(new List<float>(Layer1[i]));
                    else
                        Layer.Add(new List<float>(Layer2[i]));
                }
                else if(ver==2)
                {
                    
                    neuron.Clear();
                    for (int n = 0; n < Layer1[0].Count; n++)
                    {
                        neuron.Add((Layer1[i][n] + Layer2[i][n]) / 2);
                    }

                    Layer.Add(new List<float>(neuron));
                }
            }
            return Layer;

        }


        public float getResoult()
        {
            int ab = 0;
            float sum = 0;
            List<float> LayerLastResoult = new List<float>(inputData);
            List<float> LayerNewResoult = new List<float>();
            foreach (List<List<float>> thisLayer in Layers)
            {
                LayerNewResoult = new List<float>();
                foreach (List<float> neuron in thisLayer)
                {
                    for (int i = 0; i < neuron.Count; i++)
                    {
                        sum += LayerLastResoult[i] * neuron[i];
                    }
                    LayerNewResoult.Add(activation(sum));
                    sum = 0;

                }
                LayerLastResoult = new List<float>(LayerNewResoult);

            }
            return activation(LayerNewResoult.Sum());
        }
    }
}
