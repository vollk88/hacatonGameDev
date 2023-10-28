using AI.State;

namespace AI.Enemy.State
{
	public abstract class AEnemyStateMachine : AStateMachine 
	{
		protected AEnemy Enemy => (AEnemy) AUnit;
		
		protected AEnemyStateMachine(AUnit aUnit) : base(aUnit)
		{
			States.Add(typeof(IdleState), new IdleState(aUnit));
		}
	}

	public class IdleState : AEnemyState
	{
		public IdleState(AUnit aUnit) : base(aUnit) { }

		public override void Enter()
		{
			
		}

		public override void Update()
		{
		}

		public override void Exit()
		{
			throw new System.NotImplementedException();
		}
	}
}