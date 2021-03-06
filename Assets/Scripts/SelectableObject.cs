﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{

    public bool canIntersect = true;
    public Vector3 collisionCenter;
    public Vector3 collisionSize;
    public string objectName;
    [HideInInspector]
    public GameObject selectOverlay;
    public int cost = 0;
    [HideInInspector]
    public bool placed = false;
    [HideInInspector]
    public List<GameObject> colliders = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        var box = gameObject.AddComponent<BoxCollider>();
        box.isTrigger = true;
        box.size = collisionSize;
    }

    public void CreateOverlayObject()
    {
        if (ServiceLocator.Resolve<IGameService>().IsInPlay())
        {
            return;
        }
        selectOverlay = GameObject.CreatePrimitive(PrimitiveType.Cube);
        selectOverlay.name = "SelectionOverlay";
        selectOverlay.transform.parent = transform;
        selectOverlay.transform.localScale = collisionSize;
        selectOverlay.transform.localPosition = collisionCenter;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = new Color(Color.red.r, Color.red.g, Color.red.b, 0.5f);
        Gizmos.DrawCube(collisionCenter + transform.position, collisionSize);
    }

    void ToggleColision(bool toggle)
    {
        foreach (Collider collider in GetComponentsInChildren<Collider>())
        {
            collider.enabled = toggle;
        }
        foreach (Rigidbody rigidbody in GetComponentsInChildren<Rigidbody>())
        {
            rigidbody.useGravity = ServiceLocator.Resolve<IGameService>().IsInPlay();
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
        if (selectOverlay != null)
        {
            selectOverlay.SetActive(!placed);
        }
    }

    public void PickUp()
    {
        placed = false;

        ToggleColision(false);
        CreateOverlayObject();
    }

    public void PlaceObject()
    {
        placed = true;

        ToggleColision(true);
        if (selectOverlay != null)
        {
            Destroy(selectOverlay);
            selectOverlay = null;
        }
    }

}
