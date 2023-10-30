using BaseClasses;
using Cooking;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using CharacterController = Unit.Character.CharacterController;

namespace Input
{
	public class OpenCookingTableInput : IInput
	{
		public CharacterController Character { get; set; }
		public Transform CinemachineBrainTransform { get; set; }

		private UIManager _uiManager;
		
		public OpenCookingTableInput(CharacterController character, Transform cinemachineBrainTransform)
		{
			Character = character;
			CinemachineBrainTransform = cinemachineBrainTransform;

			_uiManager = (UIManager)CustomBehaviour.Instances[typeof(UIManager)][0];
		}

		private void Use(InputAction.CallbackContext context)
		{	
			Ray ray = new(CinemachineBrainTransform.position, CinemachineBrainTransform.forward);
			//Debug.DrawRay(ray.origin, ray.direction * INTERACTION_DISTANCE, Color.red);

			if (!Physics.Raycast(ray, out RaycastHit hit, 5)) return;
			
			if (!hit.collider.gameObject.TryGetComponent(out CookingTable table)) return;

			_uiManager ??= (UIManager)CustomBehaviour.Instances[typeof(UIManager)][0];
			_uiManager.ShowInteractionText("Использовать");
			table.OpenTable();
			UnsubscribeEvents();
			table.CookingTableClosed += SubscribeEvents;
		}

		public void SubscribeEvents()
		{
			InputManager.PlayerActions.Throw.started += Use;
		}

		public void UnsubscribeEvents()
		{
			InputManager.PlayerActions.Throw.started -= Use;
		}
	}
}