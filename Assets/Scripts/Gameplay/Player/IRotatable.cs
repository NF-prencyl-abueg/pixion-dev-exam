using UnityEngine;

public interface IRotatable
{
    void OnPlayerRotate(Vector3 rotationDirection, MovementStats movementStats);
}