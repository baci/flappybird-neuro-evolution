﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class EvolutionStats : MonoBehaviour {

	public List<float> topLengths;
	public List<float> lowLengths;
	public List<float> avgLengths;

	public List<List<float>> speciesLengths = new List<List<float>>();

	public GenerationData data = new GenerationData();


	[System.Serializable]
	public class generation{
		[XmlArray("Birds")]
		[XmlArrayItem("BirdElem")]
		public List<bird> bird = new List<bird>();
	}

	[System.Serializable]
	public class bird{
		//public float Scores;
		//public float Distances;
		//public float NumFlaps;

		public int speciesID;

		public float Fitness;

		[XmlArray("NumFlaps")]
		[XmlArrayItem("NumFlapElem")]
		public List<int> NumFlaps = new List<int>();

		[XmlArray("Distances")]
		[XmlArrayItem("DistanceElem")]
		public List<float> Distances = new List<float>();
		//public float speciesBestFitness;
	}

	public static EvolutionStats instance;
	void Awake(){
		instance = this;
	}

	private void OnApplicationQuit()
	{
		data.SaveToFile(Path.Combine(Application.persistentDataPath, "generationData.xml"));
	}

	// Use this for initialization
	void Start () {
		topLengths.Add(0);
		lowLengths.Add(0);
		avgLengths.Add(0);

		for(int i=0;i<EvolutionSettings.instance.SpecieCount;i++){
			speciesLengths.Add(new List<float>());
			speciesLengths[i].Add(0);
			//print(i);
		}
	}

	public void AnalyzeStats(){
		findTopLength();
		findLowLength();
		findAvgLength();
		findSpeciesLength();

		graphCreater.instance.UpdateGraph();
	}

	void findSpeciesLength(){
		for(int i=0;i<EvolutionSettings.instance.SpecieCount;i++){

			speciesLengths[i].Add(birdStatistics.instance.speciesBestFitness[i]);
			
			
		}
	}

	void findTopLength(){
		float top = Mathf.Max(birdStatistics.instance.Distances);
		topLengths.Add(top);
	}

	void findLowLength(){
		float low = Mathf.Min(birdStatistics.instance.Distances);
		lowLengths.Add(low);
	}

	void findAvgLength(){
		float sum =0;
		for( var i = 0; i < birdStatistics.instance.Distances.Length; i++) {
			sum += birdStatistics.instance.Distances[i];
		}
		avgLengths.Add(sum / birdStatistics.instance.Distances.Length);
	}

	public void saveData(){
		generation g = new generation();
		for(int i=0;i<birdStatistics.instance.Distances.Length;i++){
			bird b = new bird();
			//b.Distances = birdStatistics.instance.Distances[i];
			b.Fitness = birdStatistics.instance.Fitness[i];
			b.NumFlaps = gameController.instance.allBirds[i].generationStats.NumFlaps;
			b.Distances = gameController.instance.allBirds[i].generationStats.Distances;
			b.speciesID = gameController.instance.allBirds[i].speciesID;
			//b.Scores = birdStatistics.instance.Scores[i];
			g.bird.Add(b);
		}
		data.data.Add(g);

	}
}
