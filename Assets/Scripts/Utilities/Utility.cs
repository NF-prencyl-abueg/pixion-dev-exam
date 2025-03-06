using UnityEngine;

public static class Utility
{
    //Calculate the camera's normalized direction and ignores the isometric tilt of the camera
    public static Vector3 CalculateCameraDirection(Camera camera, Vector2 movementDirection)
    {
        Vector3 camForward = GetNormalizeCameraDirection(camera.transform.forward);
        Vector3 camRight = GetNormalizeCameraDirection(camera.transform.right);
        
        //Calculate the direction based on current camera orientation
        Vector3 normalizedDirection = camRight * movementDirection.x +  camForward * movementDirection.y;
        normalizedDirection.Normalize();

        return normalizedDirection;
    }
    
    //Gets the camera's normalized value, and ignores tilting 
    public static Vector3 GetNormalizeCameraDirection(Vector3 normalizedDirection)
    {
        normalizedDirection.y = 0;
        normalizedDirection.Normalize();
        return normalizedDirection;
    }
}