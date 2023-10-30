using Inventory;
using Items;
using JetBrains.Annotations;
using UnityEngine;

namespace MissionData
{
	public class InventoryTask : ATask
	{
		private EItems _itemToCollect;

		public EItems ItemToCollect
		{
			get => _itemToCollect;
			set => _itemToCollect = value;
		}
		
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
			Debug.Log(InventoryController.GetItemCountByType(_itemToCollect) + " count");
			UpdateProgress(InventoryController.GetItemCountByType(_itemToCollect));
			base.OnTaskProgressUpdatedHandler();
		}
	}
}