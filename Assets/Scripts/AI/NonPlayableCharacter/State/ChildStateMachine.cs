using AI.State;

namespace AI.NonPlayableCharacter.State
{
	public class ChildStateMachine : AStateMachine
	{
		public Child Child => (Child) AUnit;

		public ChildStateMachine(AUnit aUnit) : base(aUnit) { }
	}
}