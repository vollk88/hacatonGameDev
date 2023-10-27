using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

namespace Input
{
	public abstract class AMovementInput : IInput
	{
		private const bool IS_PLATFORMER = false;

		protected readonly WaitForFixedUpdate WaitForFixedUpdate = new();
		
		protected readonly UnitController Unit;
		protected readonly float UnitSpeed;
		protected Vector2 MoveDirection;

		private Coroutine _moveCoroutine;
		private Coroutine _jumpCoroutine;
		
		#region properties
		public bool IsMove { get; private set; }

		public bool IsJump { get; private set; }

		#endregion
		
		protected AMovementInput(UnitController unitController, float characterSpeed)
		{
			Debug.Log("Init");
			UnitSpeed = characterSpeed;
			Unit = unitController;
		}

		protected abstract IEnumerator Move();
		protected abstract IEnumerator Jump();
		
		protected virtual void StartMove(InputAction.CallbackContext context)
		{
			Debug.Log("start");
			IsMove = true;
			Vector2 readValue = context.ReadValue<Vector2>();
			MoveDirection = new Vector2(readValue.x, readValue.y) * UnitSpeed;
			
			if(_moveCoroutine != null)
				Unit.StopCoroutine(_moveCoroutine);
			_moveCoroutine = Unit.StartCoroutine(Move());
		}
		
		protected virtual void EndMove(InputAction.CallbackContext context)
		{
			Debug.Log("cancel");
			IsMove = false;
			MoveDirection = Vector3.zero;
			
			if(_moveCoroutine != null)
				Unit.StopCoroutine(_moveCoroutine);
		}

		protected virtual void JumpStarted(InputAction.CallbackContext context)
		{
			IsJump = true;
			
			if(_moveCoroutine != null)
				Unit.StopCoroutine(_moveCoroutine);
			_jumpCoroutine = Unit.StartCoroutine(Jump());
		}

		protected virtual void JumpCanceled(InputAction.CallbackContext context)
		{
			IsJump = false;
			
			if(_jumpCoroutine != null)
				Unit.StopCoroutine(_jumpCoroutine);
		}

		public void SubscribeEvents()
		{
			Debug.Log("Sub");
			InputManager.PlayerActions.Move.performed += StartMove;
			InputManager.PlayerActions.Move.canceled += EndMove;

			if (!IS_PLATFORMER) return;
			
			InputManager.PlayerActions.Jump.started += JumpStarted;
			InputManager.PlayerActions.Jump.canceled += JumpCanceled;
		}

		public void UnsubscribeEvents()
		{
			Debug.Log("Unsub");
			InputManager.PlayerActions.Move.performed -= StartMove;
			InputManager.PlayerActions.Move.canceled -= EndMove;
			if (!IS_PLATFORMER) return;
			
			InputManager.PlayerActions.Jump.started -= JumpStarted;
			InputManager.PlayerActions.Jump.canceled -= JumpCanceled;
		}

		public override string ToString()
		{
			return $"IsMove {IsMove}\nIsJump {IsJump}\n_moveVector {MoveDirection}\nUnit.Transform.position {Unit.Transform.position}";
		}
	}
}
