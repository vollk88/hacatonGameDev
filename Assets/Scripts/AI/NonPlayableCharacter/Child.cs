using AI.NonPlayableCharacter.State;
using AI.State;
using UnityEngine;
using CharacterController = Unit.Character.CharacterController;

namespace AI.NonPlayableCharacter
{
	public class Child : AUnit
	{
		[SerializeField, Tooltip("Радиус взаимодействия")]
		private float radiusInteraction = 3f;
		
		[SerializeField]
		private bool hadHat;
		[SerializeField]
		private GameObject hat;
		[SerializeField]
		private DialogueManager dialogueManager;
		
		private ChildStateMachine _stateMachine;
		private CharacterController _characterController;

		public CharacterController CharacterController =>
			_characterController ??= GetCharacterController();

		public float RadiusInteraction => radiusInteraction;
		public DialogueManager DialogueManager => dialogueManager;
		public override AStateMachine StateMachine => _stateMachine ??= new ChildStateMachine(this);
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


		protected override void Update()
		{
			base.Update();
			TryTalk();
		}

		private void TryTalk()
		{
			float distance = Vector3.Distance(transform.position,
				CharacterController.transform.position);
			
			if (distance <= RadiusInteraction &&
			    TalkState.TalkingChildCount < TalkState.MaxTalkingChildCount)
			{
				TalkState talkingChild = StateMachine.GetState<TalkState>();

				if (Time.time - talkingChild.LastTalkTime > TalkState.Cooldown &&
				    Random.Range(1, 100) < 30)
				{
					StateMachine.SetState<TalkState>();
				}
			}
		}
	}
}