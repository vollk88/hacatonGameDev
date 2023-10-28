namespace AI.NonPlayableCharacter.State
{
	public abstract class AChildState : AI.State.IState
	{
		protected readonly Child ThisChild;
		protected readonly ChildStateMachine ThisChildStateMachine;

		protected AChildState(AUnit aUnit)
		{
			ThisChild = (Child) aUnit;
			ThisChildStateMachine = (ChildStateMachine) ThisChild.StateMachine;
		}
		
		public abstract void Enter();
		public abstract void Update();
		public abstract void Exit();
	}
}