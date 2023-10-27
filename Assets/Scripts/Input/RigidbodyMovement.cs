using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

namespace Input
{
	public class RigidbodyMovement : AMovementInput
	{
		private readonly Rigidbody2D _rigidbody;
		public RigidbodyMovement(UnitController unitController, float characterSpeed) : base(unitController, characterSpeed)
		{
			if (unitController.TryGetComponent(out _rigidbody) && _rigidbody != null) return;

			_rigidbody = unitController.gameObject.AddComponent<Rigidbody2D>();
			_rigidbody.drag = 2;
			_rigidbody.angularDrag = 2;
			_rigidbody.gravityScale = 0;
		}

		protected override IEnumerator Move()
		{
			while (IsMove)
			{
				_rigidbody.velocity = MoveDirection * (UnitSpeed * Time.deltaTime);
				yield return WaitForFixedUpdate;
			}
		}

		protected override IEnumerator Jump()
		{
			throw new System.NotImplementedException();
		}

		protected override void StartMove(InputAction.CallbackContext context)
		{
			base.StartMove(context);
		}

		protected override void EndMove(InputAction.CallbackContext context)
		{
			base.EndMove(context);
		}

		protected override void JumpStarted(InputAction.CallbackContext context)
		{
			base.JumpStarted(context);
		}

		protected override void JumpCanceled(InputAction.CallbackContext context)
		{
			base.JumpCanceled(context);
		}
	}
}