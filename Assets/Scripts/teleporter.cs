using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporter : MonoBehaviour {

    public GameObject target;


	// Use this for initialization
	void Start () {
		
	}

    //if it collides with a player
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = target.transform.position;
            
        }
    }
}
