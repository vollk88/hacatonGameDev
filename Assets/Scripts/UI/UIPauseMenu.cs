using System;
using BaseClasses;
using Input;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class UIPauseMenu : CustomBehaviour
	{
		[Tooltip("Кнопка Сохранить и выйти")][SerializeField]
		private Button saveAndExitButton;

		private void Start()
		{
			saveAndExitButton.onClick.AddListener(SaveAndExit);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			Time.timeScale = 0;
			Cursor.visible = true;
			InputManager.PlayerActions.Disable();
			GameStateEvents.GamePaused?.Invoke();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			Time.timeScale = 1;
			Cursor.visible = false;
			InputManager.PlayerActions.Enable();
			GameStateEvents.GameResumed?.Invoke();
		}

		private void SaveAndExit()
		{
			throw new NotImplementedException();
		}
	}
}