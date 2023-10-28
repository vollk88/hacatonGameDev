using UnityEngine.InputSystem;

namespace Items
{
	public interface IUsable
	{
		public void Use(InputAction.CallbackContext obj);
	}
}