﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SharpNeat.Phenomes;
using SharpNeat.Core;
using SharpNeat.Genomes.Neat;

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

	public bool _isSimulating = false;
	public bool isSimulating {
		get{ return _isSimulating;}
		set{
			_isSimulating = value;
			Debug.Log("isSimulating changed to " + value);
		}
	}

	void Awake(){
		instance = this;
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

		//newRound();
	}

	public void ResetRound(List<NeatGenome> list){
		newRound();

		List<IBlackBox> _blackBoxes = new List<IBlackBox>();
		foreach (NeatGenome genome in list)
		{
			_blackBoxes.Add(genome.CachedPhenome as IBlackBox);
		}

		allBrains = _blackBoxes;

		for(int i=0;i< allBirds.Count;i++){
			allBirds[i].AI.myBrain = allBrains[i];
			if(allBrains[i]== null){
				Debug.LogError("Brain "+i+" is invalid");
			}
		}
		print("number of birds "+allBirds.Count+" number of brainz "+allBrains.Count);

		print("It worked, I guess");
		//------------------does not happen yet!------------
		
		isSimulating = true;
	}

	void newRound(){
		generation++;
		print("starting generation "+generation);

		Vector3 camPos = Camera.main.transform.position;
		camPos.x = 0;
		Camera.main.transform.position = camPos;

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
		if(!isSimulating)return;

		if(birdsAlive == 0){
			isSimulating = false;
			FlappyExperimentObject._ea.currentStatus = HaxorsEvolutionAlgorithm<NeatGenome>.SimulationStatus.ENDING;
			FlappyExperimentObject._ea.FinishGeneration();
			FlappyExperimentObject._ea.FinishGeneration();
			//newRound();
			//TODO: tell something that the generation is done
		}
	}
}