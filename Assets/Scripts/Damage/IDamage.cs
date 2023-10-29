using Damage.Strategies;

namespace Damage
{
	public interface IDamage
	{
		int Damage { get; set; }

		void GetDamage(IDamageable obj);
	}
}