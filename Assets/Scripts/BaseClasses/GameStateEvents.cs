using System;

namespace BaseClasses
{
	public static class GameStateEvents
	{
		public static Action GameStarted;
		public static Action GamePaused;
		public static Action GameResumed;
		public static Action GameEnded;
	}
}