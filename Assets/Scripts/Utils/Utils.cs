using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{

    public static Vector3 GetRandomSpawnPoint()
    {
        return new Vector3(
            Random.Range(-20, 20),
            0,
            Random.Range(-20, 20)
        );
    }

    public static Vector3 GetXZVectorFromInputVector(Vector2 inputVector) => new Vector3(inputVector.x, 0, inputVector.y);

    /// <summary>
    /// Returns a direction vector with a Y value of 0
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="destination"></param>
    /// <returns></returns>
    public static Vector3 GetXZDirectionVector(Vector3 origin, Vector3 destination)
    {

        Vector3 hitDirection = destination - origin;
        hitDirection.y = 0;
        hitDirection.Normalize();

        return hitDirection;

    }

    public static float GetXZMagnitudeFromVector(Vector3 vector, bool getSquaredMagnitude = false)
    {
        vector.y = 0;
        return getSquaredMagnitude ? vector.sqrMagnitude : vector.magnitude;
    }

}
