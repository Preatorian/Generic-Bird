using UnityEngine;
using System.Collections;
namespace Assets
{
    public class Main : MonoBehaviour {

        void Start()
        {
            Birds = GameObject.FindGameObjectsWithTag("player");
         //   Bird.GetComponent<Player>().net;
        }
        GameObject[] Birds;
        void Update()
        {
            foreach (GameObject Bird in Birds)
            {
            }
        }
    }
}
