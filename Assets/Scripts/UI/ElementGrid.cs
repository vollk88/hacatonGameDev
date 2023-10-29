using BaseClasses;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class ElementGrid : CustomBehaviour
	{
		[Tooltip("Объект с Image.")][SerializeField] 
		private Image image;

		[Tooltip("Объект с текстом кол-ва предметов.")][SerializeField] 
		private TextMeshProUGUI count;

		public void SetItem(Item item, uint itemCount)
		{
			image.sprite = item.GetIcon();
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