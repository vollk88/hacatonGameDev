using AI.State;

namespace AI.Enemy.State
{
	public abstract class AEnemyState : AUnitState
	{
		protected readonly Enemy Enemy;
		protected Perception.Perception Perception => Enemy.Perception;
		
		protected AEnemyState(AUnit aUnit) : base(aUnit)
		{
			Enemy = (Enemy) aUnit;
		}
	}
}