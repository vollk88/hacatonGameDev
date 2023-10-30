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
		[Tooltip("Кнопка Сохранить и выйти")][SerializeField]
		private Button saveAndExitButton;

		private void Start()
		{
			saveAndExitButton.onClick.AddListener(SaveAndExit);
		}

		protected override void OnEnable()
		{
			base.OnEnable();
			Time.timeScale = 0.0001f;
			Cursor.visible = true;
			InputManager.PlayerActions.Disable();
			GameStateEvents.GamePaused?.Invoke();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			Time.timeScale = 1;
			Cursor.visible = false;
			InputManager.PlayerActions.Enable();
			GameStateEvents.GameResumed?.Invoke();
		}

		private void SaveAndExit()
		{
			CharacterController characterController = GetCharacterController();
			Transform playerTransform = characterController.transform;
			
			Debug.Log($"Сохранение. Позиция: {playerTransform.position}, " +
			          $"Поворот: {playerTransform.rotation}, " +
			          $"Здоровье: {characterController.Health.CurrentHealth}, " +
			          $"Инвентарь: {InventoryController.GetItems()}");
			
			SavePrefs.Save(new SaveData(playerTransform.position, playerTransform.rotation, 
				(uint)characterController.Health.CurrentHealth, InventoryController.GetItems()));
			
			//Application.Quit();
		}
	}
}