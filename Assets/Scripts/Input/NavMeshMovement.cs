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
		private Coroutine _stepSound;
		private bool _isCorStarted;

		public NavMeshMovement(NavMeshAgent navMeshAgent, CharacterController characterController, 
			float sprintSpeed , float characterSpeed) : base(characterController, characterSpeed)
		{
			_navMeshAgent = navMeshAgent;
			_sprintSpeed = sprintSpeed;
		}
		
		protected override IEnumerator Move()
		{
			// if(_stepSound != null)
			// {
			// 	Character.StopCoroutine(StepSoundCoroutine());
			// 	_stepSound = null;
			// }

			_stepSound = Character.StartCoroutine(StepSoundCoroutine());
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

			if (_stepSound == null) yield break;
			
			Character.StopCoroutine(StepSoundCoroutine());
			_stepSound = null;
		}

		private IEnumerator StepSoundCoroutine()
		{
			if (_isCorStarted)
				yield break;
			_isCorStarted = true;
			WaitForSeconds waitForSeconds = new(0.7f);
			while (IsMove)
			{
				Character.PlayStepSound();
				yield return waitForSeconds;
			}

			_isCorStarted = false;
		} 
		
	}
}