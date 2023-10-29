using BaseClasses;
using Cinemachine;
using Input;
using Items;
using UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Unit.Character
{
	public class CharacterController : CustomBehaviour
	{
		private const float INTERACTION_DISTANCE = 4f;
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


		[SerializeField] private float throwForce = 10;
		
		[GetOnObject]
		private NavMeshAgent _navMeshAgent;

		private UIManager _uiManager;
		private IInput _movementInput;
		private IInput _throwInput;
		
		private Transform _transform;

		public Health Health => health;
		public Stamina Stamina => stamina;
		public Transform Transform => _transform;
		public Transform ThrowPoint => throwPoint;
		public float ThrowForce => throwForce;

		private GameObject _targetObject;
		private Item _targetItem;
		public GameObject TargetObject => _targetObject;
		private CinemachineBrain _cinemachineBrain;

		protected override void Awake()
		{
			_cinemachineBrain = FindObjectOfType<CinemachineBrain>();
			_transform = transform;
			_uiManager = FindObjectOfType<UIManager>();
			health.Init(_uiManager);
			stamina.Init(_uiManager, this);
			base.Awake();
			_movementInput = new NavMeshMovement(_navMeshAgent, this, sprintSpeed, moveSpeed);
			_movementInput.SubscribeEvents();
			_throwInput = new ThrowItemInput(this, _cinemachineBrain.transform);
			_throwInput.SubscribeEvents();
		}

		private void FixedUpdate()
		{
			Ray ray = new Ray(_cinemachineBrain.transform.position, _cinemachineBrain.transform.forward);
			//Debug.DrawRay(ray.origin, ray.direction * INTERACTION_DISTANCE, Color.red);

			if (!Physics.Raycast(ray, out RaycastHit hit, INTERACTION_DISTANCE, 1 << 3))
			{
				_uiManager.HideInteractionText();
				
				if (_targetObject == null) return;
				
				InputManager.PlayerActions.Take.started -= _targetObject.GetComponentInChildren<Item>().Take;
				_targetObject = null;
				_targetItem = null;
				return;
			}
			
			if (_targetObject != null && _targetObject == hit.collider.gameObject) return;
				
			//Debug.Log("Found an object - distance: " + hit.distance);
			_targetObject = hit.collider.gameObject;
			_targetItem = _targetObject.GetComponentInChildren<Item>();
			InputManager.PlayerActions.Take.started += _targetItem.Take;
			
			_uiManager.ShowInteractionText(_targetItem.GetName());
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
			_movementInput.UnsubscribeEvents();
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