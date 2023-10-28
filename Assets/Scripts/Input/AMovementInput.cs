using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.InputSystem;
using CharacterController = Unit.Character.CharacterController;

namespace Input
{
	public abstract class AMovementInput : IInput
	{
		protected readonly WaitForFixedUpdate WaitForFixedUpdate = new();

		protected readonly float UnitSpeed;
		protected Vector3 MoveDirection;

		private readonly Transform _cinemachineBrainTransform;
		private readonly CharacterController _character;
		
		private Vector2 _readValue;
		private Coroutine _moveCoroutine;
		private Coroutine _jumpCoroutine;
		
		#region properties
		public bool IsMove { get; private set; }

		public bool IsSprint { get; private set; }
		#endregion
		
		protected AMovementInput(CharacterController characterController, float characterSpeed)
		{
			_cinemachineBrainTransform = Object.FindObjectOfType<CinemachineBrain>().transform;
			UnitSpeed = characterSpeed;
			_character = characterController;
		}

		protected abstract IEnumerator Move();

		protected virtual void StartMove(InputAction.CallbackContext context)
		{
			IsMove = true;
			_readValue = context.ReadValue<Vector2>();
			SetMoveDirection();
			
			if(_moveCoroutine != null)
				_character.StopCoroutine(_moveCoroutine);
			_moveCoroutine = _character.StartCoroutine(Move());
		}
		
		protected virtual void EndMove(InputAction.CallbackContext context)
		{
			IsMove = false;
			MoveDirection = Vector3.zero;
			
			if(_moveCoroutine != null)
				_character.StopCoroutine(_moveCoroutine);
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
			Vector3 cameraForward = _cinemachineBrainTransform.forward;
			
			MoveDirection = cameraForward.normalized * _readValue.y
			                + _cinemachineBrainTransform.right.normalized * _readValue.x;

				_character.SetRotation(Quaternion.LookRotation(MoveDirection));

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
				_character.StopCoroutine(_moveCoroutine);

			InputManager.PlayerActions.Move.performed -= StartMove;
			InputManager.PlayerActions.Move.canceled -= EndMove;
			
			InputManager.PlayerActions.Sprint.started -= StartSprint;
			InputManager.PlayerActions.Sprint.canceled -= EndSprint;
		}

		public override string ToString()
		{
			return $"IsMove {IsMove}\nIsJump {IsSprint}\n_moveVector {MoveDirection}\nUnit.Transform.position {_character.Transform.position}";
		}
	}
}
