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
		public IdleState(AUnit aUnit) : base(aUnit)
		{
			throw new System.NotImplementedException();
		}

		public override void Enter()
		{
			throw new System.NotImplementedException();
		}

		public override void Update()
		{
			throw new System.NotImplementedException();
		}

		public override void Exit()
		{
			throw new System.NotImplementedException();
		}
	}
}