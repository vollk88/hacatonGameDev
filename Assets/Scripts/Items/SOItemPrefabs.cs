using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Items
{
	[CreateAssetMenu(fileName = "SOItemPrefabs", menuName = "Items/SOItemPrefabs")]
	public class SOItemPrefabs : ScriptableObject
	{
		[Serializable]
		private class PrefabByType
		{
			public EItems type;
			public GameObject prefab;
		}
		
		[Tooltip("Список prefabs предметов.")] [SerializeField]
		private List<PrefabByType> items;

		[CanBeNull]
		public GameObject Get(EItems type)
		{
			var item = items.FirstOrDefault(i => i.type == type);
			if (item is not null)
				return item.prefab;
			
			Debug.LogError($"Предмета типа {type} не существует.");
			return null;
		}
	}
}