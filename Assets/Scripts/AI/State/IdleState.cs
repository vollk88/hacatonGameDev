namespace AI.State
{
	public class IdleState : AUnitState
	{

		public IdleState(AUnit aUnit) : base(aUnit) {}
		
		public override void Enter()
		{
			ThisUnit.StopMove();
		}
		
		public override void Update()
		{
			if (ThisUnit.StateMachine.TimeToStay > 0)
			{
				ThisUnit.StateMachine.TimeToStay -= UnityEngine.Time.deltaTime;
				return;
			}
			
			if (ThisUnit.StateMachine.NextPatrolPoint is not null)
			{
				ThisUnit.StateMachine.SetState<PatrolState>();
			}
		}

		public override void Exit()
		{
			
		}
	}
}