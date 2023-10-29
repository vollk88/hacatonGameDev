using System;
using System.Collections.Generic;
using System.Linq;
using AI;
using BaseClasses;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace LevelObjects
{
	public class Spawner : CustomBehaviour
	{
		public List<AUnit> unitsToSpawn;
		
		public static void ActivateAllSpawners()
		{
			List<Spawner> spawners = Instances[typeof(Spawner)].Select(x => (Spawner) x ).ToList();
			foreach (var spawner in spawners)
			{
				spawner.Spawn();
			}
		}
		
		public void Spawn()
		{
			AUnit randomUnit = unitsToSpawn[Random.Range(0, unitsToSpawn.Count)];
			var spawnedUnit = Instantiate(randomUnit, transform.position, Quaternion.identity);
			spawnedUnit.transform.SetParent(null);
			spawnedUnit.gameObject.SetActive(true);
		}

		private void Start()
		{
			Spawn();
		}
	}
}