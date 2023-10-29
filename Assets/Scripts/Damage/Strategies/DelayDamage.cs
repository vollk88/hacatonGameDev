using BaseClasses;
using UnityEngine;

namespace Damage.Strategies
{
	public class DelayDamage : CustomBehaviour, IDamage
	{
		[SerializeField]
		private float delayTime;
		public int Damage { get; set; }

		private float _currentTime;
		
		public void GetDamage(IDamageable obj)
		{
			if (_currentTime <= 0)
			{
				obj.GetDamage(Damage);
				_currentTime = delayTime;
			}

			_currentTime -= Time.deltaTime;
		}
	}
}