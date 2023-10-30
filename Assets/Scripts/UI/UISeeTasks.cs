using BaseClasses;
using TMPro;
using UnityEngine;

namespace UI
{
	public class UISeeTasks : CustomBehaviour
	{
		[Tooltip("Текстовое поле с заданиями")][SerializeField]
		private TextMeshProUGUI tasksText;
		
		public void SetTasks(string taskDescription, uint currentValue, uint necessaryValue)
		{
			tasksText.text = taskDescription + " " + currentValue + " / " + necessaryValue;
		}
	}
}