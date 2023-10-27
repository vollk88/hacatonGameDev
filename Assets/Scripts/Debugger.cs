using Input;
using UnityEngine;
using UnityEngine.AI;
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
		
		if (Keyboard.current.tKey.wasPressedThisFrame)
			InitCmCharacter(_unitController);
	}

	private void InitRmCharacter(UnitController unitController)
	{
		NavMeshMovement cm = new(_unitController.GetComponent<NavMeshAgent>(), unitController,speed * 2, speed);
		unitController.SetMovementInput(cm);
	}

	private void InitCmCharacter(UnitController unitController)
	{
		ConsoleMovement cm = new(unitController, speed);
		unitController.SetMovementInput(cm);
	}
	
}