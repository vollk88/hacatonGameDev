using AI.State;

namespace AI.Enemy.State
{
	public abstract class AEnemyState : IState
	{
		protected AEnemy Enemy;
		
		protected AEnemyState(AUnit aUnit)
		{
			Enemy = (AEnemy) aUnit;
		}
		
		public abstract void Enter();
		public abstract void Update();
		public abstract void Exit();
	}
}