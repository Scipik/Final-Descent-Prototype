// Damage Interface
// Units that can take damage, heal, or die implement this

using UnityEngine;
using System.Collections;

public interface IDamageable<T> {
	void takeDamage(T damage);
	void healDamage(T heal);
	void onDeath();
}
