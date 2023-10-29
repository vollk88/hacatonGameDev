using AI.Enemy;
using Inventory;
using UnityEngine;
using UnityEngine.InputSystem;
using CharacterController = Unit.Character.CharacterController;

namespace Input
{
	public class ThrowItemInput : IInput
	{
		public CharacterController Character { get; set; }
		public Transform CinemachineBrainTransform { get; set; }
		private bool IsAiming { get; set; }
		
		public ThrowItemInput(CharacterController character, Transform cinemachineBrainTransform)
		{
			Character = character;
			CinemachineBrainTransform = cinemachineBrainTransform;
		}
		
		private void Use(InputAction.CallbackContext obj)
		{
			if(!IsAiming)
				return;
			
			GameObject itemPrefab = InventoryController.GetCurrentItemPrefab();
			if (itemPrefab == null)
				return;
			
			Character.SoundManager.PlaySound(1);
			
			GameObject throwItem = Object.Instantiate(InventoryController.GetCurrentItemPrefab(), 
				Character.ThrowPoint.position, Character.ThrowPoint.rotation);
			
			InventoryController.Remove(InventoryController.CurrentItem);
			
			// GameObject throwItem = GameObject.CreatePrimitive(PrimitiveType.Cube);
			// throwItem.transform.position = _throwThrowPoint.position;
			
			Rigidbody rb = throwItem.GetComponent<Rigidbody>();
			EnemySoundTrigger soundTrigger = throwItem.AddComponent<EnemySoundTrigger>();

			if (rb != null)
			{
				rb.AddForce(CinemachineBrainTransform.forward * Character.ThrowForce, ForceMode.VelocityChange);
				soundTrigger.StartTrigger();
			}
            
			IsAiming = false;
		}
		
		private void CancelAiming(InputAction.CallbackContext obj)
		{
			IsAiming = false; 
		}


		private void StartAiming(InputAction.CallbackContext obj)
		{
			IsAiming = true;
		}
		
		public void SubscribeEvents()
		{
			InputManager.PlayerActions.Throw.started += Use;
            
			InputManager.PlayerActions.Aiming.started += StartAiming;
			InputManager.PlayerActions.Aiming.canceled += CancelAiming;
		}

		public void UnsubscribeEvents()
		{
			InputManager.PlayerActions.Throw.started -= Use;
            
			InputManager.PlayerActions.Aiming.started -= StartAiming;
			InputManager.PlayerActions.Aiming.canceled -= CancelAiming;
		}
	}
}