using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using CharacterController = Unit.Character.CharacterController;

namespace Input
{
	public abstract class AMovementInput : IInput
	{
		protected readonly WaitForFixedUpdate WaitForFixedUpdate = new();

		protected readonly float UnitSpeed;
		protected Vector3 MoveDirection;

		private Vector2 _readValue;
		private Coroutine _moveCoroutine;
		private Coroutine _jumpCoroutine;
		public CharacterController Character { get; set; }
		public Transform CinemachineBrainTransform { get; set; }
		
		#region properties
		public bool IsMove { get; private set; }

		public static bool IsSprint { get; private set; }
		#endregion
		
		protected AMovementInput(CharacterController characterController, float characterSpeed)
		{
			Character = characterController;
			CinemachineBrainTransform = Object.FindObjectOfType<CinemachineBrain>().transform;
			UnitSpeed = characterSpeed;
		}

		protected abstract IEnumerator Move();

		protected virtual void StartMove(InputAction.CallbackContext context)
		{
			IsMove = true;
			_readValue = context.ReadValue<Vector2>();
			SetMoveDirection();
			
			if(_moveCoroutine != null)
				Character.StopCoroutine(_moveCoroutine);
			_moveCoroutine = Character.StartCoroutine(Move());
		}
		
		protected virtual void EndMove(InputAction.CallbackContext context)
		{
			IsMove = false;
			MoveDirection = Vector3.zero;
			
			if(_moveCoroutine != null)
				Character.StopCoroutine(_moveCoroutine);
		}

		protected virtual void StartSprint(InputAction.CallbackContext context)
		{
			IsSprint = true;
		}

		protected virtual void EndSprint(InputAction.CallbackContext context)
		{
			IsSprint = false;
		}

		protected void SetMoveDirection()
		{
			Vector3 cameraForward = CinemachineBrainTransform.forward;
			
			MoveDirection = cameraForward.normalized * _readValue.y
			                + CinemachineBrainTransform.right.normalized * _readValue.x;

				Character.SetRotation(Quaternion.LookRotation(MoveDirection));

		}


		public void SubscribeEvents()
		{
			InputManager.PlayerActions.Move.performed += StartMove;
			InputManager.PlayerActions.Move.canceled += EndMove;
			
			InputManager.PlayerActions.Sprint.started += StartSprint;
			InputManager.PlayerActions.Sprint.canceled += EndSprint;
		}

		public void UnsubscribeEvents()
		{
			if(_moveCoroutine != null)
				Character.StopCoroutine(_moveCoroutine);

			InputManager.PlayerActions.Move.performed -= StartMove;
			InputManager.PlayerActions.Move.canceled -= EndMove;
			
			InputManager.PlayerActions.Sprint.started -= StartSprint;
			InputManager.PlayerActions.Sprint.canceled -= EndSprint;
		}

		public override string ToString()
		{
			return $"IsMove {IsMove}\nIsJump {IsSprint}\n_moveVector {MoveDirection}\nUnit.Transform.position {Character.Transform.position}";
		}
	}
}
