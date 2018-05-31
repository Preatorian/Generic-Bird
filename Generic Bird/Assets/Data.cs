﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets
{
    public class Data : MonoBehaviour
    {

        // Use this for initialization
        static GameObject instance;
        static public int generation = 1;
        static public List<NeuralNet> NeuralNets;
        static public int PopulationCount = 20;
        void Awake()
        {
            if (instance)
            {
                Destroy(gameObject);
                return;
            }
            NeuralNets = new List<NeuralNet>();
            instance = gameObject;
            DontDestroyOnLoad(gameObject);
        }
    }

}
