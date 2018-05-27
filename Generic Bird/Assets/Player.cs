using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class Player : MonoBehaviour {
        private float rayLen = 2.5f;
        private Vector3 thisPos;
        private List<Vector2> allDirections;
        private List<float> Inputs;
        Rigidbody2D r;
        Text text;
        NeuralNet net;
        void Start() {
            thisPos = this.transform.position;
            Inputs = new List<float>(new float[] { 0, 0, 0, 0, 0 });
            text = GameObject.Find("Text").GetComponent<Text>();
            r = GetComponent<Rigidbody2D>();
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
                index++;
            }
            net.inputData = new List<float>(new float[] { 0,0,0,0,0,0,0,0,0,0});
            text.text = "";
            foreach (float val in Inputs)
            {
                text.text += val + "\n";
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
                r.AddForce(new Vector2(0, 300));
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                r.AddForce(new Vector2(-100, 0));
            if (Input.GetKeyDown(KeyCode.RightArrow))
                r.AddForce(new Vector2(100, 0));

            net.getResoult();
        }
    }
}
