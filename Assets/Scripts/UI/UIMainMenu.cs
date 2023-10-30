using BaseClasses;
using Unit.Character;
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
		[Tooltip("Player spawner.")][SerializeField]
		private PlayerSpawner playerSpawner;
		
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
			playerSpawner.SpawnPlayer();
		}

		private void Continue()
		{
			playerSpawner.SpawnPlayer(false);
		}
	}
}