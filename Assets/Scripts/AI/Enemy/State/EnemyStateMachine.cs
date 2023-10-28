using AI.State;

namespace AI.Enemy.State
{
	public class EnemyStateMachine : AStateMachine 
	{
		protected Enemy Enemy => (Enemy) AUnit;
		
		public EnemyStateMachine(AUnit aUnit) : base(aUnit) { }
	}
}