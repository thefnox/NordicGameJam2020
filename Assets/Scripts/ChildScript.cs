using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildScript : MonoBehaviour
{
    GameObject connector;
    ConnectorScript connectorScript;
    public Material ConnectedMaterial;
    public Material DisconnectedMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        // only triggered by objects of type connector
        if (other.gameObject.CompareTag("Connector"))
        {
            Debug.Log("Child script is currently connected to a connector");
            connector = other.gameObject;
            connectorScript = connector.gameObject.GetComponent<ConnectorScript>();
            if (connectorScript.ConnectorActiveState == true)
            {
                this.gameObject.GetComponent<Renderer>().material = ConnectedMaterial;
            }
            else if (true)
            {
                this.gameObject.GetComponent<Renderer>().material = DisconnectedMaterial;
            }
        }
    }
}
