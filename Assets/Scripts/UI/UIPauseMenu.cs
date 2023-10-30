using BaseClasses;
using Input;
using Inventory;
using UnityEngine;
using UnityEngine.UI;
using CharacterController = Unit.Character.CharacterController;

namespace UI
{
	public class UIPauseMenu : CustomBehaviour
	{
		[Tooltip("UI Manager.")][SerializeField]
		private UIManager uiManager;
		[Tooltip("Кнопка Сохранить и выйти")][SerializeField]
		private Button saveAndExitButton;
		[Tooltip("Кнопка Управление.")][SerializeField]
		private Button instructionsButton;

		private void Start()
		{
			saveAndExitButton.onClick.AddListener(SaveAndExit);
			instructionsButton.onClick.AddListener(Instructions);
		}

		private void Instructions()
		{
			uiManager.OpenTab(UIManager.EuiTabs.InstructionsTab);
		}

		protected override void OnEnable()
		{
			GameTime.Pause();
			
			InputManager.PlayerActions.Disable();
			GameStateEvents.GamePaused?.Invoke();
		}

		protected override void OnDisable()
		{
			GameTime.Resume();
			
			InputManager.PlayerActions.Enable();
			GameStateEvents.GameResumed?.Invoke();
		}

		private void SaveAndExit()
		{
			CharacterController characterController = GetCharacterController();
			Transform playerTransform = characterController.transform;
			
			/*Debug.Log($"Сохранение. Позиция: {playerTransform.position}, " +
			          $"Поворот: {playerTransform.rotation}, " +
			          $"Здоровье: {characterController.Health.CurrentHealth}, " +
			          $"Инвентарь: {InventoryController.GetItems()}");*/
			
			SavePrefs.Save(new SaveData(playerTransform.position, playerTransform.rotation, 
				(uint)characterController.Health.CurrentHealth, InventoryController.GetItems()));
			
			Application.Quit();
		}
	}
}