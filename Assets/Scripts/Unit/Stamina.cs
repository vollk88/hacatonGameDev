using UI;
using Input;
using System;
using UnityEngine;
using System.Collections;
using CharacterController = Unit.Character.CharacterController;

namespace Unit
{
	[Serializable]
	public class Stamina
	{
		[SerializeField] private int maxStamina = 100;
		[SerializeField] private int stamina = 100;

		private CharacterController _characterController;
		private UIManager _uiManager;
		private float _lastUsingTime;

		public bool IsUnusable { get; private set; }
		public int MaxStamina => maxStamina;
		public int CurrentStamina => stamina;

		public void Init(UIManager uiManager, CharacterController characterController)
		{
			_characterController = characterController;
			_uiManager = uiManager;
			stamina = maxStamina;
			_characterController.StartCoroutine(Replenish());
		}

		public void SpendOnStamina(int count)
		{
			if(IsUnusable)
				return;
			
			stamina -= count;
			_uiManager.StaminaSlider.SetSliderValue(stamina, maxStamina);
			IsUnusable = stamina <= 0;
			
			if (!IsUnusable || !(_lastUsingTime + 5 < Time.time)) return;
			_characterController.PlaySound(0);
			_lastUsingTime = Time.time;
		}

		private IEnumerator Replenish()
		{
			WaitForSeconds waitForSeconds = new(0.05f);
			while (!_characterController.Health.IsDead)
			{
				if(stamina >= maxStamina || AMovementInput.IsSprint)
				{
					yield return waitForSeconds;
					continue;
				}

				IsUnusable = false;
				stamina += 1;
				
				if (stamina >= MaxStamina)
					stamina = maxStamina;
				
				_uiManager.StaminaSlider.SetSliderValue(stamina, maxStamina);
				yield return waitForSeconds;
			}
		}
		
	}
}