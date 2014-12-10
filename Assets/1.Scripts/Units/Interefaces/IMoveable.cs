// Movement Interface
// For units that can move

using UnityEngine;
using System.Collections;

public interface IMoveable<T, nT, V, F> {
	void displayMoveableArea(); // Finds the area that the unit can move to and highlights them in green
	void removeMoveableArea(); // Removes the highlated areas
	void setMove(T moveTo); // Sets the target area
	void move(nT newTile); // Moves to target area
	IEnumerator MoveToPosition(V position, F timeToMove); // Movement Coroutine
}
