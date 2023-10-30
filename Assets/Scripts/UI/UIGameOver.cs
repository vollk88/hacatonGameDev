using BaseClasses;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
	public class UIGameOver : CustomBehaviour
	{
		[Tooltip("Кнопка выход в меню.")][SerializeField]
		private Button goToMenuButton;
		
		[Tooltip("Кнопка выход.")][SerializeField]
		private Button exitButton;

		private void Start()
		{
			goToMenuButton.onClick.AddListener(NewGame);
			exitButton.onClick.AddListener(Exit);
		}

		protected override void OnEnable()
		{
			GameTime.Pause();
		}

		protected override void OnDisable()
		{
			GameTime.Resume();
		}
		
		private void Exit()
		{
			Application.Quit();
		}

		private void NewGame()
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}