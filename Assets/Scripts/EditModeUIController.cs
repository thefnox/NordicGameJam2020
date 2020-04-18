using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditModeUIController : MonoBehaviour
{
    public SelectableObject selectionObject;
    public float selectionHeight = 0.0f;
    public bool rotationChangeMode = false;
    public bool heightChangeMode = false;
    public bool deleteMode = false;
    private Vector3 prevMousePos = Vector3.zero;
    private Vector3 referenceRotation = Vector3.zero;
    private Vector3 rotationReferenceVec = Vector3.zero;
    private Vector3 heightReferenceVec = Vector3.zero;
    public Vector3 selectionRotation = Vector3.zero;
    public RaycastHit[] raycastHits;

    public void SetSelected(GameObject obj)
    {
        var instance = Instantiate(obj);
        selectionObject = instance.GetComponent<SelectableObject>();
        selectionHeight = 1f;
        selectionRotation = Vector3.zero;
        PositionSelectionObject();
    }

    public void PlaceObject()
    {
        selectionObject.PlaceObject();
        selectionObject = null;
    }

    public void DeleteObject()
    {
        var cost = selectionObject.cost;
        Destroy(selectionObject.gameObject);
        selectionObject = null;
        // TODO: Reward back cost
    }

    public void ToggleDeleteMode(bool toggle)
    {
        deleteMode = toggle;
    }

    public void PositionSelectionObject()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (heightChangeMode)
        {
            var selectionIncrement = (prevMousePos.y - Input.mousePosition.y) / (Screen.height * 0.5f);
            selectionHeight = Mathf.Clamp(selectionHeight - selectionIncrement, -1f, 3f);
            selectionObject.transform.position = new Vector3(selectionObject.transform.position.x, selectionHeight, selectionObject.transform.position.z);
        }
        else if (rotationChangeMode)
        {
            var rotVec = new Vector2(rotationReferenceVec.y - Input.mousePosition.y, rotationReferenceVec.x - Input.mousePosition.x);
            var rotation = Mathf.Atan2(rotVec.y, rotVec.x) * Mathf.Rad2Deg;
            selectionObject.transform.rotation = Quaternion.Euler(referenceRotation + new Vector3(0f, rotation, 0f));
        }
        else
        {
            if (Physics.Raycast(ray, out hit, 2000f, LayerMask.GetMask("World")))
            {
                selectionObject.transform.position = hit.point + Vector3.up * selectionHeight;
            }
        }
    }

    public void TryPickupObject()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2000f))
        {
            var obj = hit.collider.gameObject.GetComponentInParent<SelectableObject>();
            if (obj != null)
            {
                obj.PickUp();
                selectionObject = obj;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!rotationChangeMode && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
        {
            rotationReferenceVec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
            if (selectionObject != null)
            {
                referenceRotation = selectionObject.transform.rotation.eulerAngles;
            }
        }
        if (!heightChangeMode && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            heightReferenceVec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            Destroy(selectionObject);
            selectionObject = null;
        }

        rotationChangeMode = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
        heightChangeMode = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (selectionObject == null && Input.GetButtonDown("Fire1"))
        {
            TryPickupObject();
        } else if (selectionObject != null) {
            PositionSelectionObject();
            if (Input.GetButtonDown("Fire1"))
            {
                if (deleteMode)
                {
                    DeleteObject();
                } else if (selectionObject.CanPlace())
                {

                    PlaceObject();
                }
            }
        }
        prevMousePos = Input.mousePosition;
    }
}
