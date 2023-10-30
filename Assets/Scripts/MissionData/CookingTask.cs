using System;
using Unity.VisualScripting;

namespace MissionData
{
	public class CookingTask : ATask
	{
		private string _recipe;
		public static event Action<string> Cooking; 
		
		public CookingTask(string taskDataName, string taskDataDescription, uint taskDataQuantity, string taskDataRecipe) : base(taskDataName, taskDataDescription, taskDataQuantity)
		{
			_recipe = taskDataRecipe;
		}

		public override void StartTask()
		{
			base.StartTask();
			Cooking += UpdateProgressIncrementRecipe;
		}

		private void UpdateProgressIncrementRecipe(string obj)
		{
			if (obj == _recipe)
			{
				UpdateProgressIncrement();
			}
		}

		public static void OnCooking(string recipe)
		{
			Cooking?.Invoke(recipe);
		}
	}
}