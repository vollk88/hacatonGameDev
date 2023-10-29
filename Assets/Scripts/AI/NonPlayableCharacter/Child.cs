using AI.NonPlayableCharacter.State;
using AI.State;
using Unity.VisualScripting;
using UnityEngine;
using IState = AI.State.IState;

namespace AI.NonPlayableCharacter
{
	public class Child : AUnit
	{
		[SerializeField]
		private bool hadHat;
		[SerializeField]
		private GameObject hat;
		
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
			
			// 5% что у ребенка будет шапка
			if (hadHat)
			{
				hat.SetActive(true);
			}
			else
			{
				hat.SetActive(Random.Range(1, 20) == 1);
			}
		}

		protected override void InitStates()
		{
			base.InitStates();
			StateMachine.AddState(new TalkState(this));
		}

	}
}