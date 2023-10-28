using Cinemachine;
using UnityEngine;
using CharacterController = Unit.Character.CharacterController;

namespace Input
{
	public abstract class AInput : IInput
	{
		protected readonly CharacterController Character;
		protected readonly Transform CinemachineBrainTransform;

		protected AInput(CharacterController characterController)
		{
			CinemachineBrainTransform = Object.FindObjectOfType<CinemachineBrain>().transform;
			Character = characterController;
		}

		public abstract void SubscribeEvents();
		public abstract void UnsubscribeEvents();
	}
}