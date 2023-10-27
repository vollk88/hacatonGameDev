using System;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

namespace Input
{
	public class ConsoleMovement : AMovementInput
	{
		public ConsoleMovement(UnitController unitController, float characterSpeed) : base(unitController, characterSpeed)
		{
		}

		protected override IEnumerator Move()
		{
			while (IsMove)
			{
				Debug.Log(MoveDirection);
				yield return WaitForFixedUpdate;
			}
		}

		protected override IEnumerator Jump()
		{
			throw new NotImplementedException();
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