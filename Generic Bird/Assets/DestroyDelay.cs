using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDelay : MonoBehaviour {
    
	void Start () {
		
	}
	
	void Update () {
        Destroy(gameObject,9);
	}
}
