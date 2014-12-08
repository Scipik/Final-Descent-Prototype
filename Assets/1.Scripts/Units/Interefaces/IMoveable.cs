using UnityEngine;
using System.Collections;

public interface IMoveable<T> {
	void move(T moveTo);
}
