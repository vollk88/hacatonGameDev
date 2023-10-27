using AI.NonPlayableCharacter.State;
using AI.State;

namespace AI.NonPlayableCharacter
{
	public class Child : AUnit
	{
		public override AStateMachine StateMachine { get; } = new ChildStateMachine();
		
		
	}
}