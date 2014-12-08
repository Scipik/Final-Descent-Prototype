using UnityEngine;
using System.Collections;

public interface IDamageable<T> {
	void takeDamage(T damage);
	void healDamage(T heal);
	void onDeath();
}
