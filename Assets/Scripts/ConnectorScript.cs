using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorScript : MonoBehaviour
{
    public bool ConnectorActiveState = false;
    public Material ConnectedMaterial;
    public Material DisconnectedMaterial;
    public List<GameObject> parentList;
    public List<GameObject> childList;
    ParentScript parentScript;
    ChildScript childScript;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (parentList.Count != 0)
        {
            foreach (GameObject parent in parentList)
            {
                var parentScript = parent.gameObject.GetComponent<ParentScript>();
                if (parentScript.ParentActiveState == true)
                {
                    ConnectorActiveState = true;
                }
                else
                    ConnectorActiveState = false;
            }
        }
        else if (childList.Count != 0)
        {
            foreach (GameObject child in childList)
            {
                var childScript = child.gameObject.GetComponent<ChildScript>();
                if (childScript.ChildActiveState == true)
                {
                    ConnectorActiveState = true;
                }
                else
                    ConnectorActiveState = false;
            }
        }
        else
        {
            ConnectorActiveState = false;
        }

        if (ConnectorActiveState == true)
        {
            // Debug.Log("Active state of connector " + ConnectorActiveState);
            this.gameObject.GetComponent<Renderer>().material = ConnectedMaterial;
        }
        else
            this.gameObject.GetComponent<Renderer>().material = DisconnectedMaterial;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Parent"))
        {
            if (!parentList.Contains(other.gameObject))
            {
                parentList.Add(other.gameObject);
            }
        }
        else if (other.gameObject.CompareTag("Child"))
        {
            if (!childList.Contains(other.gameObject))
            {
                childList.Add(other.gameObject);
            }
        }
    }
}
