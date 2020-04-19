using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditModeUIController : MonoBehaviour
{
    public SelectableObject selectionObject;
    public float selectionHeight = 0.0f;
    public bool rotationChangeMode = false;
    public bool heightChangeMode = false;
    public bool deleteMode = false;
    public Material selectionMaterial;
    public Material selectionDeniedMaterial;
    private Vector3 prevMousePos = Vector3.zero;
    private Vector3 referenceRotation = Vector3.zero;
    private Vector3 rotationReferenceVec = Vector3.zero;
    private GameObject parentObj;
    public Vector3 selectionRotation = Vector3.zero;
    public Text costLabel;
    public RaycastHit[] raycastHits;

    public void SetSelected(GameObject obj)
    {
        if (!parentObj)
        {
            parentObj = new GameObject("ItemContainer");
        }
        var instance = Instantiate(obj, parentObj.transform);
        selectionObject = instance.GetComponent<SelectableObject>();
        selectionHeight = 1f;
        selectionRotation = Vector3.zero;
        PositionSelectionObject();
    }

    public void PlaceObject()
    {
        var cost = selectionObject.cost;
        if (ServiceLocator.Resolve<IGameService>().GetCurrentMoney() >= cost)
        {
            selectionObject.PlaceObject();
            selectionObject = null;
            ServiceLocator.Resolve<IGameService>().AddMoney(-cost);
        }
    }

    public void SaveState()
    {
        if (parentObj != null)
        {
            ServiceLocator.Resolve<IGameService>().SaveState(parentObj.GetComponentsInChildren<SelectableObject>());
        }
    }

    public void DeleteObject()
    {
        Destroy(selectionObject.gameObject);
        selectionObject = null;
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
                var cost = obj.cost;
                obj.PickUp();
                selectionObject = obj;

                ServiceLocator.Resolve<IGameService>().AddMoney(cost);
            }
        }
    }

    public void RenderCostLabel()
    {
        if (costLabel)
        {
            if (selectionObject != null)
            {
                costLabel.enabled = true;
                var canvas = GetComponent<Canvas>();
                Vector2 pos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
                costLabel.transform.position = canvas.transform.TransformPoint(pos);
                costLabel.text = $"{selectionObject.objectName} (${selectionObject.cost})";
            } else
            {
                costLabel.enabled = false;
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
            if (!selectionObject.CanPlace())
            {
                selectionObject.selectOverlayObject.GetComponent<MeshRenderer>().material = selectionDeniedMaterial;
            }
            else
            {
                selectionObject.selectOverlayObject.GetComponent<MeshRenderer>().material = selectionMaterial;
            }
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
        RenderCostLabel();
        prevMousePos = Input.mousePosition;
    }
}
