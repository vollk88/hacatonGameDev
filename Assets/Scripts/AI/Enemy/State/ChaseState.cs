﻿using AI.State;
using UnityEngine;
using CharacterController = Unit.Character.CharacterController;

namespace AI.Enemy.State
{
	public class ChaseState : AEnemyState
	{
		private GameObject Target { get; set; }
		
		public ChaseState(AUnit aUnit) : base(aUnit)
		{
		}

		public override void Enter()
		{
			Target = Enemy.Target;
			Enemy.SetSpeed(Enemy.RunSpeed);
			Enemy.StartMove();
			Enemy.SoundManager.PlaySound(0);
		}

		public override void Update()
		{
			float distanceToTarget = Vector3.Distance(Enemy.transform.position, Target.transform.position);
			
			if (distanceToTarget < Enemy.AttackDistance)
			{
				CharacterController player = Enemy.Perception.GetCharacterController();
				if (player is not null && Target == player.gameObject)
				{
					ThisUnit.StateMachine.SetState<AttackState>();
					return;
				}
				ThisUnit.StateMachine.SetState<PatrolState>();
				return;
			}
			
			Enemy.MoveTo(Target.transform.position);
		}

		public override void Exit()
		{
			Enemy.SetSpeed(Enemy.Speed);
			Perception.TryRemoveSoundTarget();
			Target = null;
		}
	}
}
