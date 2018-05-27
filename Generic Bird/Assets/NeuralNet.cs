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
        System.Random rand;
        public NeuralNet(List<List<List<float>>> Lists)
        {
            Layers = new List<List<List<float>>>(Lists);
        }
        public NeuralNet()
        {
            rand  = new System.Random();
            List<float> nn = new List<float>(new float[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });

            List<List<float>> ii = new List<List<float>>();
            List<List<List<float>>> xx = new List<List<List<float>>>();
            
            for (int i = 0; i < 10; i++)
            {
                for (int n = 0; n < 10; n++)
                {
                    ii.Add(new List<float>(new float[] 
                    { rand.Next(-100,100),
                    rand.Next(-100,100),
                    rand.Next(-100,100),
                    rand.Next(-100,100),
                    rand.Next(-100,100),
                    rand.Next(-100,100),
                    rand.Next(-100,100),
                    rand.Next(-100,100),
                    rand.Next(-100,100),
                    rand.Next(-100,100)

                    }));
                }
                xx.Add(ii);
            }
            Layers = new List<List<List<float>>>(xx);
        }
            float activation(float x)
        {
            return (float)(1 / (1 + Math.Exp(-x)));
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
                      //  sum += ab/2f*100 * -ab*-10+20;
                        ab++;
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
