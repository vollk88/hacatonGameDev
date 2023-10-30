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
			Time.timeScale = 0.0001f;
			PlayerPrefs.SetInt("SavedGameExists", 0);
		}

		protected override void OnDisable()
		{
			Time.timeScale = 1;
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