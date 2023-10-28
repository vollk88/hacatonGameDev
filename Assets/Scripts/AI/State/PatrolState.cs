using AI.NonPlayableCharacter.State;
using UnityEngine;

namespace AI.State
{
	public class PatrolState : AUnitState
	{
		public PatrolState(AUnit aUnit) : base(aUnit) { }

		public override void Enter()
		{
			ThisUnit.StartMove();
			if (ThisUnit.StateMachine.NextPatrolPoint is null)
			{
				ThisUnit.StateMachine.SetPatrolPoint(PatrolPull.GetClosestPoint(ThisUnit.transform));
			}
			ThisUnit.OnPatrol = true;
		}

		private void SetNextPatrolPoint()
		{
			PatrolPoint closestPoint = PatrolPull.GetClosestPoint(ThisUnit.transform);
			
			ThisUnit.StateMachine.TimeToStay = closestPoint.timeToStay;
			ThisUnit.StateMachine.SetPatrolPoint(closestPoint.GetNextPoint());
		}
		
		public override void Update()
		{
			if (ThisUnit.StateMachine.NextPatrolPoint is null)
			{
				ThisUnit.StateMachine.SetState<IdleState>();
				return;
			}
			
			float distance = Vector3.Distance(ThisUnit.transform.position,
				ThisUnit.StateMachine.NextPatrolPoint.transform.position);
			
			// Debug.Log($"Distance: {distance}");
			if (distance < 1f)
			{
				SetNextPatrolPoint();
				ThisUnit.StateMachine.SetState<IdleState>();
				return;
			}
			
			ThisUnit.MoveTo(ThisUnit.StateMachine.NextPatrolPoint.transform.position);
		}

		public override void Exit()
		{
			ThisUnit.OnPatrol = false;
		}
	}
}