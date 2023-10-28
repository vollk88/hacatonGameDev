using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using CharacterController = Unit.Character.CharacterController;

namespace Input
{
	public class NavMeshMovement : AMovementInput
	{
		private readonly NavMeshAgent _navMeshAgent;
		private readonly float _sprintSpeed;

		public NavMeshMovement(NavMeshAgent navMeshAgent, CharacterController characterController, 
			float sprintSpeed , float characterSpeed) : base(characterController, characterSpeed)
		{
			_navMeshAgent = navMeshAgent;
			_sprintSpeed = sprintSpeed;
		}
		
		protected override IEnumerator Move()
		{
			while (IsMove)
			{
				SetMoveDirection();

				if (IsSprint)
					Character.Stamina.SpendOnStamina(1);

				_navMeshAgent.Move( IsSprint && !Character.Stamina.IsUnusable ?
					MoveDirection * (_sprintSpeed * Time.deltaTime) :
					MoveDirection * (UnitSpeed * Time.deltaTime));
				
				yield return WaitForFixedUpdate;
			}
		}
	}
}