using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

namespace Input
{
	public abstract class AMovementInput : IInput
	{
		protected readonly WaitForFixedUpdate WaitForFixedUpdate = new();
		
		protected readonly UnitController Unit;
		protected readonly float UnitSpeed;
		protected Vector3 MoveDirection;

		private Coroutine _moveCoroutine;
		private Coroutine _jumpCoroutine;
		
		#region properties
		public bool IsMove { get; private set; }

		public bool IsSprint { get; private set; }
		#endregion
		
		protected AMovementInput(UnitController unitController, float characterSpeed)
		{
			UnitSpeed = characterSpeed;
			Unit = unitController;
		}

		protected abstract IEnumerator Move();

		protected virtual void StartMove(InputAction.CallbackContext context)
		{
			IsMove = true;
			Vector2 readValue = context.ReadValue<Vector2>();
			MoveDirection = new Vector3(readValue.x, 0f, readValue.y);
			
			if(_moveCoroutine != null)
				Unit.StopCoroutine(_moveCoroutine);
			_moveCoroutine = Unit.StartCoroutine(Move());
		}
		
		protected virtual void EndMove(InputAction.CallbackContext context)
		{
			IsMove = false;
			MoveDirection = Vector3.zero;
			
			if(_moveCoroutine != null)
				Unit.StopCoroutine(_moveCoroutine);
		}

		protected virtual void StartSprint(InputAction.CallbackContext context)
		{
			IsSprint = true;
		}

		protected virtual void EndSprint(InputAction.CallbackContext context)
		{
			IsSprint = false;
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
			return $"IsMove {IsMove}\nIsJump {IsSprint}\n_moveVector {MoveDirection}\nUnit.Transform.position {Unit.Transform.position}";
		}
	}
}
