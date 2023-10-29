using Audio;
using BaseClasses;
using Cinemachine;
using Damage;
using Input;
using Items;
using UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Unit.Character
{
	[RequireComponent(typeof(SoundManager))]
	public class CharacterController : CustomBehaviour, IDamageable
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
		
		[GetOnObject] private NavMeshAgent _navMeshAgent;
		[GetOnObject] private SoundManager _soundManager;
		
		private UIManager _uiManager;
		private IInput _movementInput;
		private IInput _throwInput;
		private IInput _openCookingTable;
		
		private Transform _transform;

		public Terr Terr { get; set; }
		public Health Health => health;
		public Stamina Stamina => stamina;
		public Transform Transform => _transform;
		public SoundManager SoundManager => _soundManager;
		public Transform ThrowPoint => throwPoint;
		public float ThrowForce => throwForce;

		private GameObject _targetObject;
		private Item _targetItem;
		public GameObject TargetObject => _targetObject;
		private CinemachineBrain _cinemachineBrain;

		protected override void Awake()
		{
			base.Awake();

			_cinemachineBrain = FindObjectOfType<CinemachineBrain>();
			_uiManager = FindObjectOfType<UIManager>();
			_transform = transform;

			InitAndSubscribeInput();
		}

		private void InitAndSubscribeInput()
		{
			health.Init(_uiManager);
			stamina.Init(_uiManager, this);
			_movementInput = new NavMeshMovement(_navMeshAgent, this, sprintSpeed, moveSpeed);
			_movementInput.SubscribeEvents();
			_throwInput = new ThrowItemInput(this, _cinemachineBrain.transform);
			_throwInput.SubscribeEvents();
			_openCookingTable = new OpenCookingTableInput(this, _cinemachineBrain.transform);
			_openCookingTable.SubscribeEvents();
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
				InputManager.PlayerActions.Take.started -= PlayBreathSound;
				_targetObject = null;
				_targetItem = null;
				return;
			}
			
			if (_targetObject != null && _targetObject == hit.collider.gameObject) return;
				
			//Debug.Log("Found an object - distance: " + hit.distance);
			_targetObject = hit.collider.gameObject;
			_targetItem = _targetObject.GetComponentInChildren<Item>();
			InputManager.PlayerActions.Take.started += _targetItem.Take;
			InputManager.PlayerActions.Take.started += PlayBreathSound;
			
			_uiManager.ShowInteractionText(_targetItem.GetName());
		}

		private void Update()
		{
#if UNITY_EDITOR
			if(Keyboard.current.uKey.wasPressedThisFrame)
				Debug.Log(_movementInput.ToString());
#endif
		}

		private void PlayBreathSound(InputAction.CallbackContext _)
		{
			InputManager.PlayerActions.Take.started -= PlayBreathSound;
			PlaySound(2);
		}
		
		public void GetDamage(int damage)
		{
			if(health.IsDead)
				return;

			health.GetDamage(damage);
			if (health.IsDead)
				Death();
		}

		private void Death()
		{
			PlaySound(3);
			_movementInput.UnsubscribeEvents();
			GameStateEvents.GameEnded?.Invoke();
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

		public void PlaySound(int i) => _soundManager.PlaySound(i);

		public void PlayStepSound() => _soundManager.FootstepSound((int)Terr.GetMaterialIndex(_transform.position));
	}
}