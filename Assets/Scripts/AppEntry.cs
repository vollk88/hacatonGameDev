using Input;
using UI;
using UnityEngine;

public class AppEntry : MonoBehaviour
{
	[Tooltip("Объект UI Manager.")][SerializeField]
	private UIManager uiManager;
	
	private void Awake()
	{
		InputManager.EnableActions();
		uiManager.OpenTab(UIManager.EuiTabs.MainMenu);
	}

	private void OnApplicationQuit()
	{
		InputManager.DisableActions();
	}
}