using BaseClasses;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class UIInstructions : CustomBehaviour
	{
		[Tooltip("Кнопка назад.")][SerializeField]
		private Button backButton;
		
		[Tooltip("UI Manager.")][SerializeField]
		private UIManager uiManager;
		
		private void Start()
		{
			backButton.onClick.AddListener(Back);
		}

		protected override void OnEnable()
		{
			if (uiManager.IsPaused)
				GameTime.Pause();
		}
		
		private void Back()
		{
			if (uiManager.IsPaused)
			{
				uiManager.OpenTab(UIManager.EuiTabs.PauseMenu);
				return;
			}
			uiManager.OpenTab(UIManager.EuiTabs.MainMenu);
		}
	}
}