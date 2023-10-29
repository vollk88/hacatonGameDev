using BaseClasses;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class UIMainMenu : CustomBehaviour
	{
		[Tooltip("Кнопка Продолжить")][SerializeField]
		private Button continueButton;
		[Tooltip("Кнопка Новая игра")][SerializeField]
		private Button newGameButton;
		[Tooltip("Кнопка Выход")][SerializeField]
		private Button exitButton;
		
		private void Start()
		{
			continueButton.onClick.AddListener(Continue);
			newGameButton.onClick.AddListener(NewGame);
			exitButton.onClick.AddListener(Exit);
		}

		private void Exit()
		{
			Application.Quit();
		}

		private void NewGame()
		{
			throw new System.NotImplementedException();
		}

		private void Continue()
		{
			throw new System.NotImplementedException();
		}
	}
}