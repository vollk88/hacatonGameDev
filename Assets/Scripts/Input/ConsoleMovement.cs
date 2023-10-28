using UnityEngine;
using System.Collections;
using Unit.Character;
using CharacterController = Unit.Character.CharacterController;

namespace Input
{
	public class ConsoleMovement : AMovementInput
	{
		public ConsoleMovement(CharacterController characterController, float characterSpeed) : base(characterController, characterSpeed)
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