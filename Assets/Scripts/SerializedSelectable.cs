using UnityEngine;
using System.Collections;

[System.Serializable]
public class SerializedSelectable
{
    public SerializeVector3 position;
    public SerializeQuaternion rotation;
    public string name;

    public SerializedSelectable (SelectableObject obj) {
        position = new SerializeVector3(obj.transform.position);
        name = obj.objectName;
        rotation = new SerializeQuaternion(obj.transform.rotation);
    }
}
