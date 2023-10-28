namespace Input
{
	public static class InputManager
	{
		private static InputSystem _inputSystem;
		private static InputSystem.PlayerActions _playerActions;
		private static InputSystem.UIActions _uiActions;

		private static bool _actionsIsActive;

		#region Properties

		public static InputSystem.PlayerActions PlayerActions
		{
			get
			{
				if (!_actionsIsActive)
					InitActions();
				return _playerActions;

			}
		}

		public static InputSystem.UIActions UIActions
		{
			get
			{
				if (!_actionsIsActive)
					InitActions();

				return _uiActions;
			}
		}

		#endregion

		private static void InitActions()
		{
			_inputSystem = new InputSystem();
			_playerActions = _inputSystem.Player;
			_uiActions = _inputSystem.UI;
			_actionsIsActive = true;
		}

		public static void EnableActions()
		{
			PlayerActions.Enable();
			UIActions.Enable();
		}
		
		public static void DisableActions()
		{
			PlayerActions.Disable();
			UIActions.Disable();
		}
	}
}