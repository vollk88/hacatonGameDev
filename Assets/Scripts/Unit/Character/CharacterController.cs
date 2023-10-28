using System;
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
		[Header("Unit Speed.")]
		[SerializeField, Tooltip("Скорость передвижения.")]
		private float moveSpeed = 3f;
		[SerializeField, Tooltip("Скорость спринта.")]
		private float sprintSpeed = 7f;

		[Header("Health&Stamina.")]
		[SerializeField]
		private Health health;
		[SerializeField] 
		private Stamina stamina;

		[Header("Throw.")]
		[SerializeField] private Transform throwPoint;
		[SerializeField] private float throwForce = 100;
		
		[GetOnObject]
		private NavMeshAgent _navMeshAgent;

		private UIManager _uiManager;
		private IInput _movementInput;
		private ItemThrower _itemThrower;
		
		private Transform _transform;

		public Health Health => health;
		public Stamina Stamina => stamina;
		public Transform Transform => _transform;

		protected override void Awake()
		{
			_transform = transform;
			_uiManager = FindObjectOfType<UIManager>();
			health.Init(_uiManager);
			stamina.Init(_uiManager, this);
			base.Awake();
			_movementInput = new NavMeshMovement(_navMeshAgent, this, sprintSpeed, moveSpeed);
			_movementInput.SubscribeEvents();
			_itemThrower = new ItemThrower(this, throwPoint, throwForce);
			_itemThrower.SubscribeEvents();
		}

		private void Update()
		{
#if UNITY_EDITOR
			if(Keyboard.current.uKey.wasPressedThisFrame)
				Debug.Log(_movementInput.ToString());
#endif
		}

		public void GetDamage(int damage)
		{
			health.GetDamage(damage);
			if (health.IsDead)
				Death();
		}

		private void Death()
		{
			_itemThrower.UnsubscribeEvents();
			_movementInput.UnsubscribeEvents();
			throw new System.NotImplementedException();
		}

		public GameObject GetThrowableObject()
		{
			throw new NotImplementedException();
			//TODO Implement
			return new GameObject();
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