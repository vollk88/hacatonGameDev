﻿using System;
using System.Collections.Generic;
using BaseClasses;
using UnityEngine;
using CharacterController = Unit.Character.CharacterController;

namespace AI.Enemy.Perception
{
	public class Perception
	{
		public GameObject Target { get; private set; }
		
		private readonly Enemy _enemy;
		private readonly float _fovAngle;
		private readonly float _fovRadius;
		private readonly float _hearingRadius;
		private readonly Transform _fovOrigin;
		
		private Queue<GameObject> _soundTargets = new ();

		public CharacterController GetCharacterController()
		{
			if (CustomBehaviour.Instances.ContainsKey(typeof(CharacterController)) == false)
			{
				// Debug.LogError("No CharacterController found");
				return null;
			}
			
			return CustomBehaviour.Instances[typeof(CharacterController)][0] as CharacterController;
		}
		
		public Perception(Enemy enemy, float fovAngle, float fovRadius, Transform fovOrigin, float hearingRadius)
		{
			_fovAngle = fovAngle;
			_enemy = enemy;
			_fovOrigin = fovOrigin;
			_hearingRadius = hearingRadius;
			_fovRadius = fovRadius;
		}

		
		public static void SendToAllSoundTargets(GameObject soundTarget)
		{
			foreach (var enemyBehaviour in CustomBehaviour.Instances[typeof(Enemy)])
			{
				Enemy enemy = enemyBehaviour as Enemy;
				enemy!.Perception.AddSoundTarget(soundTarget);
			}
		}

		private void AddSoundTarget(GameObject soundTarget)
		{
			if (Vector3.Distance(soundTarget.transform.position, _enemy.transform.position) > _hearingRadius)
				return;
			
			_soundTargets.Enqueue(soundTarget);
		}
		
		public void TryRemoveSoundTarget()
		{
			if (_soundTargets.Count > 0 &&
			    _soundTargets.Peek() == Target)
			{
				_soundTargets.Dequeue();
			}
		}
		
		public void Update()
		{
			Target = HandleTarget(); 
		}

		private Ray RaycastToPosition(Vector3 objectPosition)
		{
			var headPosition = _fovOrigin.position;
			
			Vector3 dir = objectPosition - headPosition;
  
			return new Ray(headPosition, dir + Vector3.up);
		}
		
		protected bool IsInFOV(Vector3 objectPosition , float distance, out RaycastHit hitInfo)
		{
			Vector3 dir = objectPosition - _fovOrigin.position;         
			Vector3 headForward = Quaternion.Euler(0f, -90f, 0f) * _fovOrigin.forward;
            
			if (!(dir.magnitude <= distance 
			      && Math.Abs(Vector3.Angle(headForward, dir)) <= _fovAngle + 5f))
			{
				hitInfo = default;
				return false;
			}

			Ray ray = RaycastToPosition(objectPosition);
			var fovPosition = _fovOrigin.position;
			Debug.DrawRay(fovPosition, objectPosition - fovPosition, Color.red);
			
			return Physics.Raycast(ray, out hitInfo, distance);
		}
		
		// кидаем лучи по fow и если попадаем в персонажа, то возвращаем его
		private GameObject HandleFowCharacterRaycast()
		{
			CharacterController characterController = GetCharacterController();
			
			if (characterController is null)
				return null;
			
			Vector3 characterPosition = characterController.Transform.position + (Vector3.up);
			
			if (IsInFOV(characterPosition, _fovRadius, out RaycastHit hit))
			{
				if (hit.collider.gameObject == characterController.gameObject)
				{
					Debug.Log("Enemy see player");
					return hit.collider.gameObject;
				}
			}
			
			return null;
		}
		
		private GameObject HandleTarget()
		{
			GameObject playerInFov = HandleFowCharacterRaycast();
			if (playerInFov is not null)
				return playerInFov;
			return _soundTargets.Count > 0 ? _soundTargets.Peek() : null;
		}

	}
}