﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class birdVision : MonoBehaviour {

	RaycastHit2D[] output;

	public LayerMask mask;

	public List<Vector2> raycastDirections = new List<Vector2>();
	public List<Vector2> raycastPositions = new List<Vector2>();
	public List<float> raycastRanges = new List<float>();

	Vector2 pos;

	float[] floatOutPuts;

	// Use this for initialization
	void Awake () {
		output = new RaycastHit2D[raycastDirections.Count];

		floatOutPuts = new float[4];
		/*
		 * 0 = bird height
		 * 1 = x distance to closest pipe
		 * 2 = y difference to gap center
		 * 3 = gap y size
		 * */
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(gameController.instance.isSimulating){
			floatOutPuts[0] = transform.position.y;
			floatOutPuts[1] = pipeGenerator.instance.closestGap.transform.position.x-transform.position.x;
			floatOutPuts[2] = pipeGenerator.instance.closestGap.transform.position.y-transform.position.y;
			floatOutPuts[3] = pipeGenerator.instance.closestGap.transform.localScale.y;
		}
	}

	public float getOutputValue(int num){
		//return output[num].fraction;
		return floatOutPuts[num];
	}

	public int numOutputs(){
		return floatOutPuts.Length;
	}

	void OnDrawGizmos() {
		/*
		for(int i=0;i<raycastDirections.Count;i++){
			if(output[i].transform == null){
				Gizmos.color = Color.green;
			}else{
				Gizmos.color = Color.red;
			}
			Gizmos.DrawRay(pos+raycastPositions[i],raycastDirections[i]*raycastRanges[i]);
		}
		*/
	}
}