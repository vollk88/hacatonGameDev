using BaseClasses;
using Cinemachine;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

namespace CustomCamera
{
	public class CameraController : CustomBehaviour
	{
		private static CinemachinePOV _cinemachinePov;
		
		protected override void Awake()
		{
			base.Awake();
			// _cinemachinePov = GetComponent<CinemachinePOV>();
			// Debug.Log(_cinemachinePov == null);
			// _cinemachinePov = GetComponent<CinemachineFreeLook>().
			// 	GetRig(0).GetCinemachineComponent<CinemachinePOV>();
			// Debug.Log(_cinemachinePov == null);
			Cursor.visible = false;
		}
		
		// public static float GetXAxis()
		// {
		// 	return _cinemachinePov.m_HorizontalAxis.Value;
		// }
		//
		// public static float GetYAxis()
		// {
		// 	return _cinemachinePov.m_VerticalAxis.Value;
		// }		

	}
}