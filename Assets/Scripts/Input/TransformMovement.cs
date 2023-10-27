using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

namespace Input
{
	public class TransformMovement : AMovementInput
	{
		public TransformMovement(UnitController unitController, float characterSpeed) : base(unitController, characterSpeed)
		{
		}

		protected override IEnumerator Move()
		{
			while (IsMove)
			{
				Vector3 unitPosition = Unit.Transform.position;
				Vector3 endPosition = unitPosition + new Vector3(MoveDirection.x, MoveDirection.y, 0);

				unitPosition = Vector3.MoveTowards(unitPosition, endPosition, UnitSpeed * Time.deltaTime);
				Unit.Transform.position = unitPosition;
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