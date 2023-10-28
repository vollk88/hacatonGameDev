using UnityEngine;
using Cinemachine;
using System.Collections;
using UnityEngine.InputSystem;

namespace Input
{
	public abstract class AMovementInput : IInput
	{
		protected readonly WaitForFixedUpdate WaitForFixedUpdate = new();

		protected readonly float UnitSpeed;
		protected Vector3 MoveDirection;

		private readonly Transform _cinemachineBrainTransform;
		private readonly UnitController _unit;
		
		private Coroutine _moveCoroutine;
		private Coroutine _jumpCoroutine;
		
		#region properties
		public bool IsMove { get; private set; }

		public bool IsSprint { get; private set; }
		#endregion
		
		protected AMovementInput(UnitController unitController, float characterSpeed)
		{
			_cinemachineBrainTransform = Object.FindObjectOfType<CinemachineBrain>().transform;
			UnitSpeed = characterSpeed;
			_unit = unitController;
		}

		protected abstract IEnumerator Move();

		protected virtual void StartMove(InputAction.CallbackContext context)
		{
			IsMove = true;
			
			if(_moveCoroutine != null)
				_unit.StopCoroutine(_moveCoroutine);
			_moveCoroutine = _unit.StartCoroutine(Move());
		}
		
		protected virtual void EndMove(InputAction.CallbackContext context)
		{
			IsMove = false;
			MoveDirection = Vector3.zero;
			
			if(_moveCoroutine != null)
				_unit.StopCoroutine(_moveCoroutine);
		}

		protected virtual void StartSprint(InputAction.CallbackContext context)
		{
			IsSprint = true;
		}

		protected virtual void EndSprint(InputAction.CallbackContext context)
		{
			IsSprint = false;
		}

		protected void UpdateCharacterRotationAndMovementDirection()
		{
			_unit.SetRotation(_cinemachineBrainTransform.rotation);
			MoveDirection = _unit.Transform.forward;
			MoveDirection.y = 0;
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
			InputManager.PlayerActions.Move.performed -= StartMove;
			InputManager.PlayerActions.Move.canceled -= EndMove;
			
			InputManager.PlayerActions.Sprint.started -= StartSprint;
			InputManager.PlayerActions.Sprint.canceled -= EndSprint;
		}

		public override string ToString()
		{
			return $"IsMove {IsMove}\nIsJump {IsSprint}\n_moveVector {MoveDirection}\nUnit.Transform.position {_unit.Transform.position}";
		}
	}
}
