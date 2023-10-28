using AI.State;

namespace AI.NonPlayableCharacter.State
{
	public class ChildStateMachine : AStateMachine
	{
		public Child Child => (Child) AUnit;
		
		public PatrolPoint NextPatrolPoint { get; private set; }

		public ChildStateMachine(AUnit aUnit) : base(aUnit) { }

		public override void SetPatrolPoint(PatrolPoint point)
		{
			NextPatrolPoint = point;
		}
	}
}