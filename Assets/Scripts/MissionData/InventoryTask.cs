using Inventory;
using Items;
using JetBrains.Annotations;
using UnityEngine;

namespace MissionData
{
	public class InventoryTask : ATask
	{
		private readonly EItems _itemToCollect;
		
		public InventoryTask(string name, string description, uint quantity, EItems item)
			: base(name, description, quantity)
		{
			_itemToCollect = item;
		}

		public override void StartTask()
		{
			base.StartTask();
			Debug.Log("Inventory Task Started: " + Name + " " + Quantity + " " + _itemToCollect);
			InventoryController.InventoryChanged += OnTaskProgressUpdatedHandler;
		}

		protected override void OnTaskProgressUpdatedHandler()
		{
			UpdateProgress(InventoryController.GetItemCountByType(_itemToCollect));
			base.OnTaskProgressUpdatedHandler();
		}
	}
}