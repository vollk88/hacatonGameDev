namespace AI.NonPlayableCharacter.State
{
	public abstract class AChildState : AI.State.IState
	{
		protected Child Child;

		protected AChildState(AUnit aUnit)
		{
			Child = (Child) aUnit;
		}
		
		public abstract void Enter();
		public abstract void Update();
		public abstract void Exit();
	}
}