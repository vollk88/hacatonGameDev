using BaseClasses;
using Cinemachine;
using FMODUnity;
using UnityEngine;
using CharacterController = Unit.Character.CharacterController;

namespace CameraScript
{
	[RequireComponent(typeof(StudioListener))]
	public class CameraController : CustomBehaviour
	{
		[GetOnObject] private StudioListener _studioListener;
		[GetOnObject] private CinemachineVirtualCamera _cinemachineVirtualCamera;
		
		protected override void Awake()
		{
			base.Awake();
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		private void Start()
		{
			Transform characterTransform = FindObjectOfType<CharacterController>().Transform;
			_studioListener.SetAttenuationObject(characterTransform.gameObject);
			GetComponent<CinemachineVirtualCamera>().Follow = characterTransform;
		}
	}
}