using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class Player : MonoBehaviour {
        private float rayLen = 2f;
        private Vector3 thisPos;
        private List<Vector2> allDirections;
        private List<float> Inputs;
        Rigidbody2D r;
        Text text;
        public NeuralNet net;
        static float t1;// = Time.time;
        void Start() {
            thisPos = this.transform.position;
            Inputs = new List<float>(new float[] { 0, 0, 0, 0, 0 });
            text = GameObject.Find("Text").GetComponent<Text>();
            r = GetComponent<Rigidbody2D>();
            this.GetComponent<Animator>().speed = 2f;
            allDirections = new List<Vector2>();
            allDirections.Add(new Vector2(0, 1));
            allDirections.Add(new Vector2(1, 1));
            allDirections.Add(new Vector2(1, 0));
            allDirections.Add(new Vector2(1, -1));
            allDirections.Add(new Vector2(0, -1));

            net = new NeuralNet();
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, Vector3.down);
            
        }

        void Update()
        {
            thisPos = this.transform.position;
            RaycastHit2D hit;
            int index = 0;
            foreach (Vector2 d in allDirections)
            {
                hit = Physics2D.Raycast(thisPos, d, rayLen, 1 << 8);
                if (hit)
                {
                    Inputs[index] = hit.distance;
                }
                else
                    Inputs[index] = rayLen;
                Inputs[index] -= 1;
                index++;
            }
            net.inputData = Inputs;
            text.text = "";
            foreach (float val in Inputs)
            {
                text.text += val + "\n";
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
                Jump();
       //     List<List<float>> n1 = net.Layers[3];
        //    List<List<float>> n2 = net.Layers[5];
         //   List<List<float>> n3 = NeuralNet.crossLayers(n1,n2);
            
            text.text += net.getResoult() + "\n";
            
        }
       
        void Jump()
        {
            this.GetComponent<Animator>().Play(0);
            r.velocity = new Vector2(0,0);
            r.AddForce(new Vector2(0, 300));
        }
    }
}
