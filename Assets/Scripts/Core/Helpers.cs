using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    public static Matrix4x4 IsoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0f, 45f, 0f));

    public static Vector3 ToIso(this Vector3 input)
    {
        return IsoMatrix.MultiplyPoint3x4(input);
    }
}
