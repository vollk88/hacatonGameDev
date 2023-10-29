using BaseClasses;
using Inventory;
using UnityEngine;

namespace Unit.Character
{
	public class PlayerSpawner : CustomBehaviour
	{
		[SerializeField] private GameObject character;

		public void SpawnPlayer()
		{
			SaveData saveData = SavePrefs.Load();

			GameObject go = Instantiate(character, saveData.Position, saveData.Rotation);

			CharacterController ch = go.GetComponent<CharacterController>();
			ch.Health.CurrentHealth = (int)saveData.Health;
			InventoryController.SetItems(saveData.Inventory);

			GameStateEvents.GameStarted?.Invoke();
		}
	}
}