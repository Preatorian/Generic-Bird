using UnityEngine;
using System.Collections;
using System.Threading;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

namespace Assets
{
    public class Main : MonoBehaviour {
        float time;
        public GameObject obsticlePrefab,birdPrefab;
        GameObject obsticle,birdInstance;
        Vector3 pos;
        System.Random rand;
        Quaternion q;
        Text text;
        GameObject[] Birds, blocks;
        List<NeuralNet> Nets;
        void Start()
        {
            //Y : -1.95 >> 4.05f
            Nets = Data.NeuralNets;
            text = GameObject.Find("Text").GetComponent<Text>();
            time = Time.timeSinceLevelLoad;
            q = new Quaternion();
            rand = new System.Random();
            pos = new Vector3(8,-19.5f,0);
            time = Time.time;
            blocks = GameObject.FindGameObjectsWithTag("block");
            foreach (GameObject t in blocks) t.SetActive(false);
            if(Data.NeuralNets.Count == Data.PopulationCount)
                evolutionAlgorythm();
            for(int i = 0; i < Data.PopulationCount; i++)
            {
                birdInstance = Instantiate(birdPrefab, new Vector3(-6,2,0), q);
                if (Data.NeuralNets.Count == Data.PopulationCount)// if yes then this is not the first generation
                    birdInstance.GetComponent<Player>().net = Data.NeuralNets[i];
            }
            Birds = GameObject.FindGameObjectsWithTag("Player");

        }

        private int evolutionAlgorythm()
        {
            Nets.Sort((x,y) => y.fitness.CompareTo(x.fitness));
            for (int i = 19; i >= 10; i--)
                Nets.RemoveAt(i);
            for(int i=0; i<10; i+=2)
            {
                Nets.Add(crossover(Nets[i], Nets[i + 1], 2));
                Nets.Add(crossover(Nets[i+1], Nets[i], 2));
            }
            return 0;
        }
        private NeuralNet crossover(NeuralNet net1,NeuralNet net2,int ver)
        {
            NeuralNet localNet = new NeuralNet(net1.Layers, GetInstanceID());
            //    for (int i=0; i<NeuralNet.layersCount;i++)
            //   {
            int i = rand.Next(NeuralNet.layersCount);
            localNet.Layers[i] = NeuralNet.crossLayers(net1.Layers[i], net2.Layers[i],rand.Next(NeuralNet.neuronCount), ver);
            localNet.mutate(i);
        //    }
            return localNet;
        }

        void Update()
        {
            if(Time.time-time>2.5)
            {
                pos.y = (float)(-1.95 + rand.Next(0,3) * 2);
                Instantiate(obsticlePrefab, pos, q);
                time = Time.time;
            }
            int len = GameObject.FindGameObjectsWithTag("Player").Length;
            if (len == 0)
            {
                Data.generation++;
                SceneManager.LoadScene("MainScene");
            }

            text.text = $"generation:{Data.generation}\n Birds Alive:{len}";
        }
    }
}
