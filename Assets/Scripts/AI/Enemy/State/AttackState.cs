using System;
using AI.State;
using BaseClasses;
using Cinemachine;
using UnityEngine;
using CharacterController = Unit.Character.CharacterController;

namespace AI.Enemy.State
{
	public class AttackState : AEnemyState
	{
		// ивент атаки
		public event Action<bool> OnAttack; 
		public AttackState(AUnit aUnit) : base(aUnit)
		{
			Target = CustomBehaviour.GetCharacterController();
		}

		private CharacterController Target { get; set; } 
			
		public override void Enter()
		{
			Enemy.StopMove();
			OnAttack?.Invoke(true);
			Enemy.SoundManager.PlaySound(1);
		}

		public override void Update()
		{
			Enemy.LookAt(Target.transform.position);
			float distanceToTarget = Vector3.Distance(Enemy.transform.position, Target.transform.position);
			// _attackDelay -= Time.deltaTime;
			// if (_attackDelay <= 0)
			// {
			// 	Target.GetDamage(Enemy.AttackDamage);
			// 	_attackDelay = Enemy.AttackDelay;
			// }
			Enemy.DamageStrategy.GetDamage(Target);
			
			if (distanceToTarget > Enemy.AttackDistance)
			{
				if (Enemy.Target is null)
				{
					ThisUnit.StateMachine.SetState<IdleState>();
				}
				else
				{
					ThisUnit.StateMachine.SetState<ChaseState>();
				}
				return;
			}
		}

		public override void Exit()
		{
			OnAttack?.Invoke(false);
		}
	}
}