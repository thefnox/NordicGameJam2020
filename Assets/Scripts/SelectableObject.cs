using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{

    public bool canIntersect = true;
    public Material selectionMaterial;
    public Material selectionDeniedMaterial;
    public Vector3 collisionCenter;
    public Vector3 collisionSize;
    public GameObject selectOverlayObject;
    public int cost = 0;
    [HideInInspector]
    public bool placed = false;
    public List<GameObject> colliders = new List<GameObject>();
    private GameObject selectObjectInstance;

    // Start is called before the first frame update
    void Start()
    {
        ToggleColision(false);
        var box = gameObject.AddComponent<BoxCollider>();
        box.isTrigger = true;
        box.size = collisionSize;
        selectObjectInstance = Instantiate(selectOverlayObject, transform);
        selectObjectInstance.transform.localScale = collisionSize;
        selectObjectInstance.transform.localPosition = collisionCenter;
    }

    void ToggleColision(bool toggle)
    {
        foreach (Collider collider in GetComponentsInChildren<Collider>())
        {
            collider.enabled = toggle;
        }
        foreach (Rigidbody rigidbody in GetComponentsInChildren<Rigidbody>())
        {
            rigidbody.useGravity = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (placed) return;

        if (!colliders.Contains(other.gameObject))
        {
            if (other.gameObject.tag == "World")
            {
                colliders.Add(other.gameObject);
            } else if (!canIntersect)
            {
                colliders.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (placed) return;

        if (colliders.Contains(other.gameObject))
        {
            colliders.Remove(other.gameObject);
        }
    }


    public bool CanPlace()
    {
        return colliders.Count == 0 && !placed;
    }

    public void Update()
    {
        if (selectObjectInstance != null)
        {
            selectObjectInstance.SetActive(!placed);
            if (!CanPlace())
            {

                selectObjectInstance.GetComponent<MeshRenderer>().material = selectionDeniedMaterial;
            }
            else
            {
                selectObjectInstance.GetComponent<MeshRenderer>().material = selectionMaterial;
            }
        }
    }

    public void PickUp()
    {
        placed = false;

        ToggleColision(false);
    }

    public void PlaceObject()
    {
        placed = true;

        ToggleColision(true);
    }
}
