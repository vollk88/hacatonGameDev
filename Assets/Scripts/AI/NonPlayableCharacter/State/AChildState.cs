using BaseClasses;
using UnityEngine;
using CharacterController = Unit.Character.CharacterController;

namespace AI.NonPlayableCharacter.State
{
	public abstract class AChildState : AI.State.AUnitState
	{
		protected readonly Child ThisChild;
		protected readonly ChildStateMachine ThisChildStateMachine;

		protected AChildState(AUnit aUnit) : base(aUnit)
		{
			ThisChild = (Child)aUnit;
			ThisChildStateMachine = (ChildStateMachine) ThisChild.StateMachine;
		}
		
		// public abstract void Enter();
		public override void Update()
		{
			// float distance = Vector3.Distance(ThisChild.transform.position,
			// 	CharacterController.transform.position);
			//
			// if (distance <= ThisChild.RadiusInteraction &&
			//     TalkState.TalkingChildCount < TalkState.MaxTalkingChildCount)
			// {
			// 	// if (Random.Range(1, 5) == 1)
			// 		ThisChildStateMachine.SetState<TalkState>();
			// }
		}
		// public abstract void Exit();
	}
}