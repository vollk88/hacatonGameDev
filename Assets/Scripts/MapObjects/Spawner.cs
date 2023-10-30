using System.Collections.Generic;
using System.Linq;
using AI;
using BaseClasses;
using UnityEngine;
using CharacterController = Unit.Character.CharacterController;
using Random = UnityEngine.Random;

namespace MapObjects
{
	public class Spawner : CustomBehaviour
	{
		[SerializeField]
		private bool showGizmos;
		
		private const int FrameToCheck = 30;
		private const float RadiusToPlayer = 50f;
		public List<AUnit> unitsToSpawn;
		
		private bool _locked;
		private int _frameCount;
		private Transform _playerTransform;
		private CharacterController Player { get; set; }
		
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
			GameStateEvents.GameStarted += Init;
		}

		private void Init()
		{
			Player = CustomBehaviour.GetCharacterController();
			_playerTransform = Player.Transform;
		}

		private void Update()
		{
			if (Player is null)
				return;
			
			if (_locked || _frameCount < FrameToCheck)
			{
				return;
			}
			_frameCount = 0;
			
			if (Vector3.Distance(_playerTransform.position, transform.position) > RadiusToPlayer) 
				return;
			
			Spawn();
			_locked = true;
		}


		private void FixedUpdate()
		{
			if (_locked) return;
			
			_frameCount++;
		}

		#if UNITY_EDITOR 
		private void OnDrawGizmos()
		{
			if (!showGizmos) return;
			
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(transform.position, RadiusToPlayer);
		}
		#endif
	}
}