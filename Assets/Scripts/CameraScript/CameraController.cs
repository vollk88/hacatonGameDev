using BaseClasses;
using Cinemachine;
using UnityEngine;
using CharacterController = Unit.Character.CharacterController;
using Cursor = UnityEngine.Cursor;

namespace CameraScript
{
	public class CameraController : CustomBehaviour
	{
		protected override void Awake()
		{
			base.Awake();
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			GetComponent<CinemachineVirtualCamera>().Follow = FindObjectOfType<CharacterController>().Transform;
		}
	}
}