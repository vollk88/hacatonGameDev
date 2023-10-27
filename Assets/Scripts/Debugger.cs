using System;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

public class Debugger : MonoBehaviour
{
	private UnitController _unitController;
	[SerializeField] private float speed = 1f;
	private void Start()
	{
		_unitController = gameObject.GetComponent<UnitController>();
	}

	private void Update()
	{
		if (Keyboard.current.rKey.wasPressedThisFrame)
			InitRmCharacter(_unitController);

		if(Keyboard.current.tKey.wasPressedThisFrame)
			InitTmCharacter(_unitController);
		
		if (Keyboard.current.yKey.wasPressedThisFrame)
			InitCmCharacter(_unitController);
	}

	private void InitCmCharacter(UnitController unitController)
	{
		ConsoleMovement cm = new(unitController, speed);
		unitController.SetMovementInput(cm);
	}
	
	private void InitTmCharacter(UnitController unitController)
	{
		TransformMovement rm = new(unitController, speed);
		unitController.SetMovementInput(rm);
	}
	
	private void InitRmCharacter(UnitController unitController)
	{
		RigidbodyMovement rm = new(unitController, speed);
		unitController.SetMovementInput(rm);
	}
}