namespace AI.NonPlayableCharacter.State
{
	public abstract class AChildState : AI.State.AUnitState
	{
		protected readonly Child ThisChild;
		protected readonly ChildStateMachine ThisChildStateMachine;

		protected AChildState(AUnit aUnit) : base(aUnit)
		{
			ThisChild = (Child)aUnit;
			ThisChildStateMachine = (ChildStateMachine) ThisChild.StateMachine;
		}
		
		// public abstract void Enter();
		// public abstract void Update();
		// public abstract void Exit();
	}
}