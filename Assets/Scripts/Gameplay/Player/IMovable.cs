using UnityEngine;

public interface IMovable
{
    void OnPlayerMove(Vector3 movementDirection, MovementStats movementStats);
}