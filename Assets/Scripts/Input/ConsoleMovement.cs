using UnityEngine;
using System.Collections;

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
	}
}