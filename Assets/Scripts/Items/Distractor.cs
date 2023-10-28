using Cinemachine;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using CharacterController = Unit.Character.CharacterController;

namespace Items
{
	public class Distractor : AItem, IUsable, IInput
	{
		[Tooltip("Тип")][SerializeField]
		private EDistractors type;
		public EDistractors Type => type;
		
		public CharacterController Character { get; set; }
		public Transform CinemachineBrainTransform { get; set; }
		public bool IsAiming { get; private set; }

		protected override void Awake()
		{
			base.Awake();
			Character = FindObjectOfType<CharacterController>();
			CinemachineBrainTransform = FindObjectOfType<CinemachineBrain>().transform;
		}
		
		public void Use()
		{
			if(!IsAiming)
				return;
			
			GameObject throwItem = Object.Instantiate(Character.GetThrowableObject(), 
				Character.ThrowPoint.position, Character.ThrowPoint.rotation);
			
			// GameObject throwItem = GameObject.CreatePrimitive(PrimitiveType.Cube);
			// throwItem.transform.position = _throwThrowPoint.position;
			
			Rigidbody rb = throwItem.AddComponent<Rigidbody>();

			if (rb != null)
				rb.AddForce(CinemachineBrainTransform.forward * Character.ThrowForce, ForceMode.VelocityChange);
			IsAiming = false;
		}
		
		public void Use(InputAction.CallbackContext obj)
		{
			throw new System.NotImplementedException();
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