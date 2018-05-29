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
        static System.Random rand;
        public NeuralNet(List<List<List<float>>> Lists)
        {
            Layers = new List<List<List<float>>>(Lists);
        }
        public NeuralNet()
        {
            rand  = new System.Random();
            List<float> nn = new List<float>(new float[] { 0, 0, 0, 0, 0});

            List<List<float>> ii = new List<List<float>>();
            List<List<List<float>>> xx = new List<List<List<float>>>();
            
            for (int i = 0; i < 1; i++)
            {
                for (int n = 0; n < 5; n++)
                {
                    ii.Add(new List<float>(new float[] 
                    { 
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
        float activation(float x)
        {
            return (float)(1 / (1 + Math.Exp(-x)));
        }
        public void mutate(int min, int max)
        {
            int LayersCount = Layers.Count(),
                NeuronCount = Layers[1].Count(),
                NeuronsCapacity = Layers[1][0].Count(),
                r1 = rand.Next(0, LayersCount),
                r2 = rand.Next(0, NeuronCount),
                r3 = rand.Next(0, NeuronsCapacity);
                Layers[r1][r2][r3] = rand.Next(min, max);
        }
        public static List<List<float>> crossLayers(List<List<float>> Layer1, List<List<float>> Layer2)
        {
            List<List<float>> Layer = new List<List<float>>();
            int len = Math.Min(Layer2.Count,Layer1.Count),
                mid = rand.Next(len);
            for(int i = 0; i < len; i++)
            {
                if (mid < i)
                    Layer.Add(new List<float>(Layer1[i]));
                else
                    Layer.Add(new List<float>(Layer2[i]));
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
