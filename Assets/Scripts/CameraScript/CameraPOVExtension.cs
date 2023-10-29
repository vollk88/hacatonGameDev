using Cinemachine;
using Input;
using UnityEngine;

namespace CameraScript
{
	public class CameraPovExtension : CinemachineExtension
	{
		[SerializeField] private Vector3 startingRotation;
		[SerializeField] private float verticalSpeed;
		[SerializeField] private float horizontalSpeed;
		[SerializeField] private float clampAngle = 60;
		protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
		{
			if (!vcam.Follow) return;
			
			if (stage != CinemachineCore.Stage.Aim) return;
			
			if (startingRotation == Vector3.zero)
				startingRotation = transform.localRotation.eulerAngles;
			
			Vector2 deltaInput = InputManager.PlayerActions.Look.ReadValue<Vector2>();
			startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
			startingRotation.y -= deltaInput.y * horizontalSpeed * Time.deltaTime;
			startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
			state.RawOrientation = Quaternion.Euler(startingRotation.y, startingRotation.x, 0f);
		}
	}
}