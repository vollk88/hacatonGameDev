using BaseClasses;
using Items;
using TMPro;
using UI.Cooking;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class ElementGrid : CustomBehaviour
	{
		[Tooltip("SO Sprites Image предметов.")][SerializeField]
		private SOItemIcons itemIcons;
		
		[Tooltip("Объект с Image.")][SerializeField] 
		private Image image;

		[Tooltip("Объект с текстом кол-ва предметов.")][SerializeField] 
		private TextMeshProUGUI count;

		public void SetItem(Item item, uint itemCount)
		{
			image.sprite = itemIcons.Get(item.Type);
			count.text = itemCount.ToString();
			image.enabled = true;
			count.enabled = true;
		}
		
		public void Hide()
		{
			image.enabled = false;
			count.enabled = false;
		}
	}
}