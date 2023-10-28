using AI.NonPlayableCharacter.State;
using AI.State;
using Unity.VisualScripting;
using UnityEngine;
using IState = AI.State.IState;

namespace AI.NonPlayableCharacter
{
	public class Child : AUnit
	{
		private ChildStateMachine _stateMachine;

		public override AStateMachine StateMachine => _stateMachine ??= new ChildStateMachine(this);

		protected override void Awake()
		{
			base.Awake();
			Debug.Log("Child awake");
		}

		protected override void Start()
		{
			base.Start();
			StateMachine.SetPatrolPoint(PatrolPull.GetClosestPoint(transform));
			StateMachine.SetState<IdleState>();
		}

		protected override void InitStates()
		{
			base.InitStates();
			StateMachine.AddState(new TalkState(this));
		}

		protected override void Update()
		{
			base.Update();
		}
	}
}