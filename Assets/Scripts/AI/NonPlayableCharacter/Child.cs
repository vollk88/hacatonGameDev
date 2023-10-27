using AI.NonPlayableCharacter.State;
using AI.State;

namespace AI.NonPlayableCharacter
{
	public class Child : AUnit
	{
		private ChildStateMachine _stateMachine;

		public override AStateMachine StateMachine => _stateMachine ??= new ChildStateMachine(this);
	}
}