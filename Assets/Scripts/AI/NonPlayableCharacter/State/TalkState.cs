using System;
using AI.State;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AI.NonPlayableCharacter.State
{
	public class TalkState : AChildState
	{
		public const int MaxTalkingChildCount = 3;
		public const float Cooldown = 10f;
		private const float MaxTalkTime = 3f;
		
		
		public static int TalkingChildCount = 0;
		private float _talkTime = 0f;
		
		public TalkState(AUnit aUnit) : base(aUnit)
		{ }

		public float LastTalkTime { get; private set; }
		
		public override void Enter()
		{
			_talkTime = 0f;
			
			TalkingChildCount++;
			ThisChild.StopMove();
			ThisChild.LookAt(ThisChild.CharacterController.transform.position);
			TryGetDialogue();
		}

		private void TryGetDialogue()
		{
			// if (Random.Range(1, 5) != 1)
			// 	return;
			
			StartDialogue();
		}

		private void StartDialogue()
		{
			ThisChild.DialogueManager.gameObject.SetActive(true);
			ThisChild.DialogueManager.UpdatePhrase();
		}

		public override void Update()
		{
			base.Update();
			_talkTime += Time.deltaTime;
			if (_talkTime >= MaxTalkTime)
			{
				ThisChildStateMachine.SetState<IdleState>();
			}	
		}

		public override void Exit()
		{
			TalkingChildCount--;
			ThisChild.StartMove();
			ThisChild.DialogueManager.gameObject.SetActive(false);
			LastTalkTime = Time.time;
		}
	}
}