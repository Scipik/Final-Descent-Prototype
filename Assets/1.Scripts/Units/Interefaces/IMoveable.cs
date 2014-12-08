using UnityEngine;
using System.Collections;

public interface IMoveable<T> {
	void displayMoveableArea();
	void move(T moveTo);
}
