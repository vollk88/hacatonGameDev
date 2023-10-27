using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Input
{
	public class NavMeshMovement : AMovementInput
	{
		private readonly NavMeshAgent _navMeshAgent;
		private readonly float _sprintSpeed;

		public NavMeshMovement(NavMeshAgent navMeshAgent, UnitController unitController, 
			float sprintSpeed , float characterSpeed) : base(unitController, characterSpeed)
		{
			_navMeshAgent = navMeshAgent;
			_sprintSpeed = sprintSpeed;
		}
		
		protected override IEnumerator Move()
		{
			while (IsMove)
			{
				UpdateCharacterRotationAndMovementDirection();

				_navMeshAgent.Move( IsSprint ?
					MoveDirection * (_sprintSpeed * Time.deltaTime) :
					MoveDirection * (UnitSpeed * Time.deltaTime));
				yield return WaitForFixedUpdate;
			}
		}
	}
}