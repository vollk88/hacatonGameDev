namespace AI.State
{
	public abstract class AUnitState : IState
	{
		protected readonly AUnit ThisUnit;
		
		protected AUnitState(AUnit aUnit)
		{
			ThisUnit = aUnit;
		}


		public abstract void Enter();
		public abstract void Update();
		public abstract void Exit();
	}
}