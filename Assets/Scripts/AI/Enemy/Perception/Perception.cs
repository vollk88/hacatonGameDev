using System.Collections.Generic;
using BaseClasses;
using UnityEngine;
using CharacterController = Unit.Character.CharacterController;

namespace AI.Enemy.Perception
{
	public class Perception
	{
		private readonly Enemy _enemy;
		private float _fowAngle;
		private float _fowRadius;
		private float _hearingRadius;
		private readonly Transform _fowOrigin;
		
		private Queue<GameObject> _soundTargets = new ();

		public CharacterController GetCharacterController()
		{
			if (CustomBehaviour.Instances[typeof(CharacterController)].Count == 0)
			{
				// Debug.LogError("No CharacterController found");
				return null;
			}
			
			return CustomBehaviour.Instances[typeof(CharacterController)][0] as CharacterController;
		}
		
		public Perception(Enemy enemy, float fowAngle, float fowRadius, Transform fowOrigin, float hearingRadius)
		{
			_fowAngle = fowAngle;
			_enemy = enemy;
			_fowOrigin = fowOrigin;
			_hearingRadius = hearingRadius;
			_fowRadius = fowRadius;
		}

		public GameObject Target { get; private set; }
		
		public void AddSoundTarget(GameObject soundTarget)
		{
			if (Vector3.Distance(soundTarget.transform.position, _enemy.transform.position) > _hearingRadius)
				return;
			
			_soundTargets.Enqueue(soundTarget);
		}
		
		public void TryRemoveSoundTarget()
		{
			if (_soundTargets.Peek() == Target)
				_soundTargets.Dequeue();
		}
		
		public void Update()
		{
			Target = HandleTarget(); 
		}

		// кидаем лучи по fow и если попадаем в персонажа, то возвращаем его
		private GameObject HandleFowCharacterRaycast()
		{
			CharacterController characterController = GetCharacterController();

			var fowPosition = _fowOrigin.position;
			Ray ray = new Ray(fowPosition, characterController.Transform.position - fowPosition);
			
			for (int i = 0; i < 5; i++)
			{
				float angle = _fowAngle / 2f;
				float angleStep = _fowAngle / 5f;
				ray.direction = Quaternion.Euler(0, -angle + angleStep * i, 0) * ray.direction;
				if (Physics.Raycast(ray, out RaycastHit hit, _fowRadius))
				{
					Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
					if (hit.collider.gameObject == characterController.gameObject)
					{
						return hit.collider.gameObject;
					}
				}
			}
			
			return null;
		}
		
		private GameObject HandleTarget()
		{
			return HandleFowCharacterRaycast() ?? _soundTargets.Peek();
		}

	}
}