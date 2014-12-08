// Movement Interface
// For units that can move

using UnityEngine;
using System.Collections;

public interface IMoveable<T> {
	void displayMoveableArea(); // Finds the area that the unit can move to and highlights them in green
	void move(T moveTo); // Moves to target area
}
