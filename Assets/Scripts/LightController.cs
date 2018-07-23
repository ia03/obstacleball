﻿using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {
	public GameObject player;
	private Vector3 offset;


	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}

	// Update is called once per frame
	void LateUpdate () {
		transform.position = offset + player.transform.position;
	}
}
