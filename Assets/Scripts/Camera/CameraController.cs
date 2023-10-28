using BaseClasses;
using Cinemachine;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

namespace CustomCamera
{
	public class CameraController : CustomBehaviour
	{
		protected override void Awake()
		{
			base.Awake();
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}
}