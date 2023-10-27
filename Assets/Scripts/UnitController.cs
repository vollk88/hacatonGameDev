using Input;
using UnityEngine;
using BaseClasses;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class UnitController : CustomBehaviour
{
	[Header("Unit Speed")]
	[SerializeField, Tooltip("Скорость передвижения.")]
	private float moveSpeed = 3f;
	[SerializeField, Tooltip("Скорость спринта.")]
	private float sprintSpeed = 7f;
	private IInput _movementInput;
	[GetOnObject]
	private NavMeshAgent _navMeshAgent;

	private Transform _transform;

	public Transform Transform => _transform;

	protected override void Awake()
	{
		_transform = transform;
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