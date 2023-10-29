using Input;
using Unit.Character;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using CharacterController = Unit.Character.CharacterController;

public class Debugger : MonoBehaviour
{
	private CharacterController _characterController;
	[SerializeField] private float speed = 1f;
	private void Start()
	{
		_characterController = FindObjectOfType<CharacterController>();
	}

	private void Update()
	{
#if UNITY_EDITOR
		if (Keyboard.current.rKey.wasPressedThisFrame)
			InitRmCharacter(_characterController);
		if (Keyboard.current.yKey.wasPressedThisFrame)
			_characterController.GetDamage(5);
		
		if (Keyboard.current.tKey.wasPressedThisFrame)
			InitCmCharacter(_characterController);
#endif
	}

	private void InitRmCharacter(CharacterController characterController)
	{
		NavMeshMovement cm = new(_characterController.GetComponent<NavMeshAgent>(), characterController,speed * 2, speed);
		characterController.SetMovementInput(cm);
	}

	private void InitCmCharacter(CharacterController characterController)
	{
		ConsoleMovement cm = new(characterController, speed);
		characterController.SetMovementInput(cm);
	}
	
}