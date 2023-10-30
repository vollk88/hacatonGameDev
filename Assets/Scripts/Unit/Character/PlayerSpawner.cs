using System.Collections.Generic;
using BaseClasses;
using Inventory;
using Items;
using UnityEngine;

namespace Unit.Character
{
	public class PlayerSpawner : CustomBehaviour
	{
		[SerializeField] private GameObject character;
		[SerializeField] private GameObject virtualCamera;

		public void SpawnPlayer(bool isNewGame = true)
		{
			SaveData saveData = isNewGame ? 
				new SaveData(transform.position, Quaternion.identity, 
					100, new Dictionary<Item, uint>()) : SavePrefs.Load();

			GameObject go = Instantiate(character, saveData.Position, saveData.Rotation);

			CharacterController ch = go.GetComponent<CharacterController>();
			ch.Health.CurrentHealth = (int)saveData.Health;
			InventoryController.SetItems(saveData.Inventory);

			PlayerPrefs.SetInt("SavedGameExists", 1);
			virtualCamera.SetActive(true);
			GameStateEvents.GameStarted?.Invoke();
		}
	}
}