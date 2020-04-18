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
    private List<GameObject> connectorList;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script initialized");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerStay(Collider other)
    {
        if (!connectorList.Contains(other.gameObject))
        {
            connectorList.Add(other.gameObject);
        }

        // only triggered by objects of type connector
        if ((other.gameObject.CompareTag("Connector")) && (ParentActiveState == true))

        {
            Debug.Log("Starter script is currently connected to a connector");
            StarterObject.GetComponent<Renderer>().material = ConnectedMaterial;
            foreach(GameObject connector in connectorList)
            {
                connectorScript = connector.gameObject.GetComponent<ConnectorScript>();
                connectorScript.ConnectorActiveState = true;
            }
        }
        else if (true)
        {
            StarterObject.GetComponent<Renderer>().material = DisconnectedMaterial;
            connectorScript.ConnectorActiveState = false;
        }
    }
}
