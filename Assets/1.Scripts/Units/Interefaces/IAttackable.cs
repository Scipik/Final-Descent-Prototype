using UnityEngine;
using System.Collections;

public interface IAttackable<T, Y> {
	void attack(T obj);
	void specialAttack(Y obj);
}
