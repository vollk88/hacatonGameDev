using Unity.VisualScripting;
using UnityEngine;

namespace AI.Enemy.State
{
	public class ChaseState : AEnemyState
	{
		public ChaseState(AUnit aUnit) : base(aUnit)
		{
		}

		public override void Enter()
		{
			Enemy.StartMove();
			
		}

		public override void Update()
		{
			float distanceToTarget = Vector3.Distance(Enemy.transform.position, Perception.Target.transform.position);
			
			if (distanceToTarget < 1f)
			{

				if (Perception.Target == Perception.GetCharacterController().gameObject)
				{
					ThisUnit.StateMachine.SetState<AttackState>();
				}
			}
		}

		public override void Exit()
		{
			Perception.TryRemoveSoundTarget();
		}
	}
}