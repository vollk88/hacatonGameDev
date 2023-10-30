using System.Collections.Generic;
using Items;
using Newtonsoft.Json;
using UnityEngine;

namespace BaseClasses
{
	public class SaveData
	{
		public Vector3 Position;
		public Quaternion Rotation;
		public uint Health;
		public Dictionary<Item, uint> Inventory;
		
		public SaveData(Vector3 position, Quaternion rotation, uint health, Dictionary<Item, uint> inventory)
		{
			Position = position;
			Rotation = rotation;
			Health = health;
			Inventory = inventory;
		}
	}
	
	public class SavePrefs : CustomBehaviour
	{
		public static void Save(SaveData saveData)
		{
			//Сохранение показателя здоровья
			PlayerPrefs.SetInt("Health", (int) saveData.Health);
			
			//Сохранения позиции в мире
			PlayerPrefs.SetFloat("PositionX", saveData.Position.x);
			PlayerPrefs.SetFloat("PositionY", saveData.Position.y);
			PlayerPrefs.SetFloat("PositionZ", saveData.Position.z);
			
			//Сохранение поворота в мире
			PlayerPrefs.SetFloat("RotationX", saveData.Rotation.x);
			PlayerPrefs.SetFloat("RotationY", saveData.Rotation.y);
			PlayerPrefs.SetFloat("RotationZ", saveData.Rotation.z);
			
			//Сохранение инвентаря
			string jsonInventory = JsonConvert.SerializeObject(saveData.Inventory);
			PlayerPrefs.SetString("Inventory", jsonInventory);
			
			PlayerPrefs.SetInt("SavedGameExists", 1);
			
			//TODO сохранение статуса заданий
		}

		public static SaveData Load()
		{
			//Загрузка показателя здоровья
			uint health = (uint) PlayerPrefs.GetInt("Health");
			
			//Загрузка позиции в мире
			float positionX = PlayerPrefs.GetFloat("PositionX");
			float positionY = PlayerPrefs.GetFloat("PositionY");
			float positionZ = PlayerPrefs.GetFloat("PositionZ");
			Vector3 position = new Vector3(positionX, positionY, positionZ);
			
			//Загрузка поворота в мире
			float rotationX = PlayerPrefs.GetFloat("RotationX");
			float rotationY = PlayerPrefs.GetFloat("RotationY");
			float rotationZ = PlayerPrefs.GetFloat("RotationZ");
			Quaternion rotation = new Quaternion(rotationX, rotationY, rotationZ, 0);
			
			//Загрузка инвентаря
			string jsonInventory = PlayerPrefs.GetString("Inventory");
			Dictionary<Item, uint> inventory = JsonConvert.DeserializeObject<Dictionary<Item, uint>>(jsonInventory);
			
			//TODO загрузка статуса заданий
			
			return new SaveData(position, rotation, health, inventory);
		}
	}
}