using System;
using Input;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitController : MonoBehaviour
{
	private IInput _movementInput;

	private Transform _transform;

	public Transform Transform => _transform;
	
	/*public UnitController()
	{
		_movementInput = new ConsoleMovement(this, 3);
	}
	
	public UnitController(IInput movementInput)
	{
		_movementInput = movementInput;
	}*/

	private void Awake()
	{
		_transform = transform;
		_movementInput = new ConsoleMovement(this, 3);
	}

	private void Update()
	{
		if(Keyboard.current.uKey.wasPressedThisFrame)
			Debug.Log(_movementInput.ToString());
	}

	public void SetMovementInput(IInput movementInput)
	{
		_movementInput.UnsubscribeEvents();
		_movementInput = movementInput;
		_movementInput.SubscribeEvents();
	}
}