using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBotController : MonoBehaviour {

    private Rigidbody rb;

    private Vector3 movement = new Vector3(0, 0, 0);
    public float speed = 50;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	
	void FixedUpdate () {
        // Building of force vector 
        movement = new Vector3(0.0f, 0.0f, -1.0f);
        // Adding force to rigidbody
        rb.AddForce(movement * speed * 2.5f * Time.deltaTime);
    }
}
