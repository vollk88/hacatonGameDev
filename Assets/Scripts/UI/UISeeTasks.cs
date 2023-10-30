using BaseClasses;
using TMPro;
using UnityEngine;

namespace UI
{
	public class UISeeTasks : CustomBehaviour
	{
		[Tooltip("Текстовое поле с заданиями")][SerializeField]
		private TextMeshProUGUI tasksText;

		public GameObject TaskTextObject => tasksText.gameObject;

		private void Start()
		{
			GameStateEvents.GameStarted += () => tasksText.gameObject.SetActive(true);
			GameStateEvents.GameResumed += () => tasksText.gameObject.SetActive(true);

			GameStateEvents.GamePaused += () => tasksText.gameObject.SetActive(false);
			GameStateEvents.GameEnded += () => tasksText.gameObject.SetActive(false);
			GameStateEvents.GameWin += () => tasksText.gameObject.SetActive(false);
		}

		public void SetTasks(string taskDescription, uint currentValue, uint necessaryValue)
		{
			tasksText.text = taskDescription + " " + currentValue + " / " + necessaryValue;
		}
	}
}