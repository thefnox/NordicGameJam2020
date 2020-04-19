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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ParentActiveState = true;
        }
    }
}
