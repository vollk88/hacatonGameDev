using Unity.VisualScripting;
using UnityEngine;

namespace AI.NonPlayableCharacter.State
{
	public class PatrolState : AChildState
	{
		public PatrolState(AUnit aUnit) : base(aUnit) { }

		public override void Enter()
		{
			ThisChild.StartMove();
			if (ThisChildStateMachine.NextPatrolPoint is null)
			{
				ThisChildStateMachine.SetPatrolPoint(PatrolPull.GetClosestPoint(ThisChild.transform));
			}
			ThisChild.OnPatrol = true;
		}

		private void SetNextPatrolPoint()
		{
			PatrolPoint closestPoint = PatrolPull.GetClosestPoint(ThisChild.transform);
			
			ThisChildStateMachine.TimeToStay = closestPoint.timeToStay;
			ThisChildStateMachine.SetPatrolPoint(closestPoint.GetNextPoint());
		}
		
		public override void Update()
		{
			if (ThisChildStateMachine.NextPatrolPoint is null)
			{
				ThisChildStateMachine.SetState<IdleState>();
				return;
			}
			
			float distance = Vector3.Distance(ThisChild.transform.position,
				ThisChildStateMachine.NextPatrolPoint.transform.position);
			
			Debug.Log($"Distance: {distance}");
			if (distance < 1f)
			{
				SetNextPatrolPoint();
				ThisChildStateMachine.SetState<IdleState>();
				return;
			}
			
			ThisChild.MoveTo(ThisChildStateMachine.NextPatrolPoint.transform.position);
		}

		public override void Exit()
		{
			ThisChild.OnPatrol = false;
		}
	}
}