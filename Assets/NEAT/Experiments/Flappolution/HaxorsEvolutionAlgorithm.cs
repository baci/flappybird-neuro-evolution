﻿using UnityEngine;
using SharpNeat.Phenomes;
using SharpNeat.EvolutionAlgorithms.ComplexityRegulation;
		Debug.Log("Performing a generation. Status = " + _currentStatus.ToString());
									  ISpeciationStrategy<TGenome> speciationStrategy,
									  IComplexityRegulationStrategy complexityRegulationStrategy):base(eaParams, speciationStrategy,complexityRegulationStrategy){
	}