using UnityEngine;

namespace BaseClasses
{
	public class GameTime
	{
		public static void Pause()
		{
			Time.timeScale = 0.0001f;
		}
		
		public static void Resume()
		{
			Time.timeScale = 1;
		}
	}
}