﻿using UnityEngine;
using System.Collections;

public class moveWall : MonoBehaviour {

	public float speed;



	// Update is called once per frame
	void Update () {
		

		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + (speed * Time.deltaTime));
	}
}
