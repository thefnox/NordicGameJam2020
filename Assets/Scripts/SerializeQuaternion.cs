using UnityEngine;

[System.Serializable]
public class SerializeQuaternion
{
    public float x;
    public float y;
    public float z;
    public float w;

    public SerializeQuaternion(Quaternion quaternion)
    {
        x = quaternion.x;
        y = quaternion.y;
        z = quaternion.z;
        w = quaternion.w;
    }
}
