using System.Collections.Generic;
using BaseClasses;

namespace Damage.Strategies
{
	public class MeleeDamage : CustomBehaviour, IDamage
	{
		public int Damage
		{
			get => _damage; 
			set
			{
				_damage = value;

				if (_collisionDamages.Count == 0)
				{
					TryInitCollisionDamageInRoot();
				}

				foreach (var collisionDamage in _collisionDamages)
				{
					collisionDamage.Damage = _damage;
				}
			}
		}

		private void TryInitCollisionDamageInRoot()
		{
			foreach (var damage in GetComponentsInChildren<CollisionDamage>())
			{
				_collisionDamages.Add(damage);
			}
		}
		
		private readonly List<CollisionDamage> _collisionDamages = new ();
		private int _damage;
		
		
		public void GetDamage(IDamageable obj) 
		{
			/*
				Урон проходит в CollisionDamage.cs
			*/ 
		}
	}
}