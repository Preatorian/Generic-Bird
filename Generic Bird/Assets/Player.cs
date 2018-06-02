using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets
{
    public class Player : MonoBehaviour {
        private float rayLen = 8f;
        private Vector3 thisPos;
        private List<Vector2> allDirections;
        private List<float> Inputs;
        Rigidbody2D r;
        Text text;
        public NeuralNet net;
        Rigidbody2D rigid;
        Vector3 rotation;
        static float t1;// = Time.time;
        static float time = 0;
        float resoult = 0;
        void Start()
        {
            rotation = transform.eulerAngles;
            rigid = GetComponent<Rigidbody2D>();
            thisPos = this.transform.position;
            Inputs = new List<float>(new float[NeuralNet.neuronCount]);
            text = GameObject.Find("Text").GetComponent<Text>();
            r = GetComponent<Rigidbody2D>();
            this.GetComponent<Animator>().speed = 2f;
            allDirections = new List<Vector2>();
            allDirections.Add(new Vector2(0, 1));
            allDirections.Add(new Vector2(1, 1));
            allDirections.Add(new Vector2(1, 0));
            allDirections.Add(new Vector2(1, -1));
            allDirections.Add(new Vector2(0, -1));
            if (Data.NeuralNets.Count < Data.PopulationCount)
            {
                net = new NeuralNet(GetInstanceID());
                Data.NeuralNets.Add(net);
            }
        }
        void Update()
        {

            rotation.z = rigid.velocity.y*5;
            transform.eulerAngles = rotation;
            thisPos = this.transform.position;
            updateInputs();
            net.inputData = Inputs;
            /*text.text = "";
            
            foreach (float val in Inputs)
            {
                text.text += val + "\n";
            }*/
            try
            {
                resoult = net.getResoult();
            }
            catch
            {
                Debug.Log("net is empty!");
            }
           // text.text += resoult;
            if(resoult > 0) Jump();

            //if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0))
            //    Jump();
            //text.text += "Time: " + System.Math.Round(Time.timeSinceLevelLoad, 2).ToString(new CultureInfo("pl-PL")) + "\n";
            //text.text += "Best Time: "+ time + "\n";
        }
       
        void Jump()
        {
            this.GetComponent<Animator>().Play(0);
            r.velocity = new Vector2(0,0);
            r.AddForce(new Vector2(0, 300));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            net.fitness = Time.timeSinceLevelLoad;
            Destroy(gameObject);
            /*if(Time.timeSinceLevelLoad > time)
                time = Time.timeSinceLevelLoad;*/
        }
        void updateInputs()
        {
            int index = 0;
            RaycastHit2D hit;
            foreach (Vector2 d in allDirections)
            {
                hit = Physics2D.Raycast(thisPos, d, rayLen, 1 << 8);
                if (hit)
                {
                    Inputs[index] = hit.distance;
                }
                else
                    Inputs[index] = rayLen;
                Inputs[index] = Inputs[index] * 2 / rayLen - 1;
                index++;
            }
        }
    }
}
