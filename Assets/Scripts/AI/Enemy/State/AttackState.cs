using AI.State;
using Cinemachine;
using UnityEngine;
using CharacterController = Unit.Character.CharacterController;

namespace AI.Enemy.State
{
	public class AttackState : AEnemyState
	{
		public AttackState(AUnit aUnit) : base(aUnit)
		{
			Target = Enemy.Perception.GetCharacterController();
		}

		private CharacterController Target { get; set; } 
		private float _attackDelay;
			
		public override void Enter()
		{
			Enemy.StopMove();
			_attackDelay = Enemy.AttackDelay;
		}

		public override void Update()
		{
			float distanceToTarget = Vector3.Distance(Enemy.transform.position, Target.transform.position);
			Debug.DrawRay(Enemy.transform.position, Target.transform.position - Enemy.transform.position, Color.red);
			_attackDelay -= Time.deltaTime;
			if (_attackDelay <= 0)
			{
				Target.GetDamage(Enemy.AttackDamage);
				_attackDelay = Enemy.AttackDelay;
			}
			
			if (distanceToTarget > Enemy.AttackDistance)
			{
				ThisUnit.StateMachine.SetState<IdleState>();
				return;
			}
		}

		public override void Exit()
		{
			
		}
	}
}