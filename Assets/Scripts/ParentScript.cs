using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentScript : MonoBehaviour
{
    public GameObject StarterObject;
    public Material ConnectedMaterial;
    public Material DisconnectedMaterial;
    public bool ParentActiveState;
    GameObject connector;
    ConnectorScript connectorScript;
    public List<GameObject> connectorList;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script initialized");
    }

    // Update is called once per frame
    void Update()
    {
       if (ParentActiveState == true)
        {
            StarterObject.GetComponent<Renderer>().material = ConnectedMaterial;
        }
        else
        {
            StarterObject.GetComponent<Renderer>().material = DisconnectedMaterial;
        }
    }

    /* private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.CompareTag("Connector")) && (ParentActiveState == true))
        {
            if (!connectorList.Contains(other.gameObject))
            {
                connectorList.Add(other.gameObject);
            }
        }
    }
    */
}
