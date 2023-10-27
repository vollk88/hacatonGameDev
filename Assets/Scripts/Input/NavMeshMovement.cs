using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Camera;

namespace Input
{
	public class NavMeshMovement : AMovementInput
	{
		private readonly NavMeshAgent _navMeshAgent;
		private readonly float _sprintSpeed;

		public NavMeshMovement(NavMeshAgent navMeshAgent, UnitController unitController, float sprintSpeed , float characterSpeed) : base(
			unitController, characterSpeed)
		{
			_navMeshAgent = navMeshAgent;
			_sprintSpeed = sprintSpeed;
		}

		private void Test()
		{
			float horizontal = CameraController.GetXAxis();
			float vertical = CameraController.GetYAxis();
			Debug.Log(horizontal + " " + vertical);
			//
			// // Вычисляем направление движения относительно камеры
			// Vector3 cameraForward = Vector3.Scale(freeLookCam.transform.forward, new Vector3(1, 0, 1)).normalized;
			// Vector3 moveDirection = cameraForward * vertical + freeLookCam.transform.right * horizontal;
		}
		
		protected override IEnumerator Move()
		{
			while (IsMove)
			{
				Test();
				_navMeshAgent.Move( IsSprint ?
					MoveDirection * (_sprintSpeed * Time.deltaTime) :
					MoveDirection * Time.deltaTime);
				yield return WaitForFixedUpdate;
			}
		}
	}
}