using AI.State;

namespace AI.NonPlayableCharacter.State
{
	public class ChildStateMachine : AStateMachine
	{
		public override AUnit AUnit { get; protected set; }

		public override void Init(AUnit aUnit)
		{
			base.Init(aUnit);
			States.Add(typeof(IdleState), new IdleState(aUnit));
		}
	}
}