// Attack interface
// Units can attack implement this

using UnityEngine;
using System.Collections;

public interface IAttackable<T, U> {
	bool displayAttackableUnits();
	void removeAttackableUnits();
	void setAttack(T tile); // Sets Action Node to attack unit on tile
	void attack(U target); // Attacks the unit
	IEnumerator AttackUnit(U target);
}
