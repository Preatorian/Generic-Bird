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
        GameObject[] Birds, blocks, o;
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
            Nets.Sort((x,y) => new System.Random((int)Time.time).Next());
            Nets.Sort((x,y) => y.fitness.CompareTo(x.fitness));
            for (int i = Data.PopulationCount-1; i >= Data.PopulationCount/2; i--)
                Nets.RemoveAt(i);
            for(int i=0; i< Data.PopulationCount / 2; i+=2)
            {
                Nets.Add(Crossover(Nets[i], Nets[i + 1], 1));
                if(Data.PopulationCount>Nets.Count)
                    Nets.Add(Crossover(Nets[i+1], Nets[i], 2));
            }

            return 0;
        }
        public NeuralNet Crossover(NeuralNet net1,NeuralNet net2,int ver)
        {
            NeuralNet localNet = new NeuralNet(new List<List<List<float>>>(),0);
            for (int i=0; i<NeuralNet.layersCount;i++)
            {
                localNet.Layers.Add(NeuralNet.CrossLayers(net1.Layers[i], net2.Layers[i],rand.Next(NeuralNet.neuronCount), ver));
                rand = new System.Random(rand.Next());
                for (int n=0;n<rand.Next(1,4);n++)
                {
                    rand = new System.Random(rand.Next());
                    localNet.mutate(i, GetInstanceID() + n);

                }
            }
            return localNet;
            
        }
        int a = 0;
        void FixedUpdate()
        {
            if (Data.bestFitness < Time.timeSinceLevelLoad)
                Data.bestFitness = Time.timeSinceLevelLoad;
            if (Time.fixedTime-time>3)
            {
                a += 1;//rand.Next(10);
                rand = new System.Random(rand.Next());
                pos.y = (float)(-1.95 + a % 3 + 2); //rand.Next(0, 3) * 2+1
                Instantiate(obsticlePrefab, pos, q);
                /*o = GameObject.FindGameObjectsWithTag("Finish");
                if(o.Length>1)
                Debug.Log(o[0].transform.position - o[1].transform.position);*/
                time = Time.fixedTime;
            }
            int len = GameObject.FindGameObjectsWithTag("Player").Length;
            if (len == 0)
            {
                Data.generation++;
                SceneManager.LoadScene("MainScene");
            }

            text.text = $"Generation:{Data.generation}\n" +
                        $"Birds Alive:{len}\n" +
                        $"Fitness: {Time.timeSinceLevelLoad.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}\n" +
                        $"Best: {Data.bestFitness.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}\n";
        }
    }
}
