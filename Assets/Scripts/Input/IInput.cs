
using UnityEngine;

namespace Input
{
	public interface IInput
	{
		public Unit.Character.CharacterController Character { get; set; }
		public Transform CinemachineBrainTransform { get; set; }
		
		void SubscribeEvents();
		void UnsubscribeEvents();
	}
}