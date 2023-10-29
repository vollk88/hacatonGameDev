using UnityEngine.InputSystem;

namespace Items
{
	public interface IItem
	{
		public void Take(InputAction.CallbackContext obj);
	}
}