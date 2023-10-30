using System;
using BaseClasses;
using UnityEngine;
using CharacterController = Unit.Character.CharacterController;

public class ViewOnCharacter : MonoBehaviour
{
	private CharacterController _characterController;

	private void Start()
	{
		GameStateEvents.GameStarted += Init;
	}

	private void Init()
	{
		_characterController = CustomBehaviour.GetCharacterController();
	}

	private void Update()
	{
		if (_characterController is null)
			return;
		transform.LookAt(_characterController.transform.position);
		transform.Rotate(0, 180, 0);
	}
}
