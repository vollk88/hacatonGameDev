using System.Collections;
using System.Collections.Generic;
using BaseClasses;
using Cinemachine;
using Inventory;
using Items;
using MissionData;
using UnityEngine;

namespace Unit.Character
{
	public class PlayerSpawner : CustomBehaviour
	{
		[SerializeField] private GameObject character;
		[SerializeField] private GameObject virtualCamera;

		public void SpawnPlayer(bool isNewGame = true)
		{
			SaveData saveData;
			
			if (isNewGame)
			{
				saveData = new SaveData(transform.position, Quaternion.identity,
					100, new Dictionary<EItems, uint>(), new List<ATask>());
				PlayerPrefs.SetInt("SavedGameExists", 0);
			}
			else
			{
				saveData = SavePrefs.Load();
			}
			GameObject go = Instantiate(character, saveData.Position, saveData.Rotation);

			CharacterController ch = go.GetComponent<CharacterController>();
			ch.Health.CurrentHealth = (int)saveData.Health;
			InventoryController.SetItems(saveData.Inventory);
			
			virtualCamera.SetActive(true);
			GameStateEvents.GameStarted?.Invoke();
			StartCoroutine(CameraFov());
		}

		private IEnumerator CameraFov()
		{
			var vc = virtualCamera.GetComponent<CinemachineVirtualCamera>();
			var maxFOV = 60f;
			while (vc.m_Lens.FieldOfView++ <= maxFOV) 
				yield return new WaitForEndOfFrame();
		}
	}
}