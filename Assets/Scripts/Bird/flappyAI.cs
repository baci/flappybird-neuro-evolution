﻿using UnityEngine;
using System.Collections;


public class flappyAI : MonoBehaviour {
	birdController BC;



	// Use this for initialization
	void Start () {
		BC = GetComponent<birdController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(BC.vision.getOutputValue(0) < 2f && BC.vision.getOutputValue(2) > 0.5f){
		
			float chance = Random.Range(0,100);
			if(chance < 5){
				BC.Flap();
			}
			if(BC.vision.getOutputValue(0) < -3f || BC.vision.getOutputValue(2) > -0.5f){
				BC.Flap();
			}
		}
	}
}