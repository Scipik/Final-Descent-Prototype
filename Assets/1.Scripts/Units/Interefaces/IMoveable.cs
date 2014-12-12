// Movement Interface
// For units that can move

using UnityEngine;
using System.Collections;

public interface IMoveable<I, G, T, V, F> {
	void displayMoveableArea(); // Finds the area that the unit can move to and highlights them in green
	void removeMoveableArea(); // Removes the highlated areas
	void setMove(G moveTo); // Sets the target area
	void move(T fromTile, T toTile, I apCost); // Moves to target area
	IEnumerator MoveToPosition(V fromPos, V toPos, F timeToMove); // Movement Coroutine
}
