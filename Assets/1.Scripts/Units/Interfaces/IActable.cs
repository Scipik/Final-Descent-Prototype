// Actor Interface
// Used for units that act on a turn

using UnityEngine;
using System.Collections;

public interface IActable<A> {
	void activate(); // Sets values when this units turn comes up
	void cancelAction(); // Cancels last set action (Only for player characters)
	void doAction(A obj);
}
