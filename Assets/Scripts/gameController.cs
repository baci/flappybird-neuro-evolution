﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SharpNeat.Phenomes;
using SharpNeat.Core;

public class gameController : MonoBehaviour {

	public float gravity;
	public float forwardSpeed;

	int generation = 0;

	public int numBirds;

	public GameObject birdPre;

	public int birdsAlive;

	GameObject birdHolder;

	public List<SharpNeat.Phenomes.IBlackBox> allBrains = new List<SharpNeat.Phenomes.IBlackBox>();

	public List<birdController> allBirds = new List<birdController>();

	public static gameController instance;
	void Awake(){
		instance = this;
		HaxorsEvolutionAlgorithm<IBlackBox>.OnGenerationStarted += IDontEvenCare;
	}

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = -1;
			
		Physics2D.gravity = new Vector2(0,-gravity);

		birdHolder = new GameObject();
		birdHolder.name = "birdHolder";

		birdStatistics.instance.Init(numBirds);

		for(int i=0;i< numBirds;i++){
			GameObject g = Instantiate(birdPre) as GameObject;
			g.transform.parent = birdHolder.transform;
			birdController bc = g.GetComponent<birdController>();
			bc.ID = i;
			setBirdStats(bc);
			allBirds.Add(bc);
		}

		newRound();
	}

	public void IDontEvenCare(List<IGenome<IBlackBox>> list){
		allBrains = list;
	}

	void newRound(){
		generation++;
		print("starting generation "+generation);

		Vector3 campos = Camera.main.transform.position;
		campos.x = 0;
		Camera.main.transform.position = campos;

		pipeGenerator.instance.Clear();
		pipeGenerator.instance.GenerateStart();

		birdStatistics.instance.Clear();
		birdsAlive = numBirds;

		foreach(birdController bc in allBirds){
			ResetBird(bc);
			setBirdStats(bc);
		}


	}

	void ResetBird(birdController bc){
		if(bc.vision){
			bc.vision.enabled = true;
		}
		bc.transform.position *= 0;
		bc.dead = false;
		bc.velocity *= 0;
		bc.points = 0;
		bc.enabled = true;

	}

	void setBirdStats(birdController bc){

	}

	// Update is called once per frame
	void Update () {
		if(birdsAlive == 0){
			newRound();
		}
	}
}