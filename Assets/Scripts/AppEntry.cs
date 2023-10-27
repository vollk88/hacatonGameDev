using Input;
using UnityEngine;

public class AppEntry : MonoBehaviour
{
	private void Awake()
	{
		InputManager.EnableActions();
	}

	private void OnApplicationQuit()
	{
		InputManager.DisableActions();
	}
}