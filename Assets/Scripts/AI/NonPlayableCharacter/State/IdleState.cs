namespace AI.NonPlayableCharacter.State
{
	public class IdleState : AChildState
	{

		public IdleState(AUnit aUnit) : base(aUnit) {}
		
		public override void Enter()
		{
			ThisChild.StopMove();
		}
		
		public override void Update()
		{
			if (ThisChildStateMachine.TimeToStay > 0)
			{
				ThisChildStateMachine.TimeToStay -= UnityEngine.Time.deltaTime;
				return;
			}
			
			if (ThisChildStateMachine.NextPatrolPoint is not null)
			{
				ThisChildStateMachine.SetState<PatrolState>();
			}
		}

		public override void Exit()
		{
			
		}
	}
}