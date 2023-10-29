using System;
using BaseClasses;
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
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			Time.timeScale = 1;
		}

		private void SaveAndExit()
		{
			throw new NotImplementedException();
		}
	}
}