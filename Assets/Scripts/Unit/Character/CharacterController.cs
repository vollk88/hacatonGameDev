using BaseClasses;
using Input;
using UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Unit.Character
{
	public class CharacterController : CustomBehaviour
	{
		[Header("Unit Speed")]
		[SerializeField, Tooltip("Скорость передвижения.")]
		private float moveSpeed = 3f;
		[SerializeField, Tooltip("Скорость спринта.")]
		private float sprintSpeed = 7f;

		[Header("Health&Stamina")]
		[SerializeField]
		private Health health;
		[GetOnObject]
		private NavMeshAgent _navMeshAgent;

		private UIManager _uiManager;
		private IInput _movementInput;
		
		private Transform _transform;

		public Transform Transform => _transform;

		protected override void Awake()
		{
			_transform = transform;
			_uiManager = FindObjectOfType<UIManager>();
			health.Init(_uiManager);
			base.Awake();
			_movementInput = new NavMeshMovement(_navMeshAgent, this, sprintSpeed, moveSpeed);
			_movementInput.SubscribeEvents();
		}

		private void Update()
		{
#if UNITY_EDITOR
			if(Keyboard.current.uKey.wasPressedThisFrame)
				Debug.Log(_movementInput.ToString());
#endif
		}

		public void GetDamage(float damage)
		{
			health.GetDamage(damage);
			if (health.IsDeath)
				Death();
		}

		private void Death()
		{
			throw new System.NotImplementedException();
		}

		public void SetRotation(Quaternion rotation)
		{
			Transform.rotation = rotation;
		}

		public void SetMovementInput(IInput movementInput)
		{
			_movementInput.UnsubscribeEvents();
			_movementInput = movementInput;
			_movementInput.SubscribeEvents();
		}
	}
}