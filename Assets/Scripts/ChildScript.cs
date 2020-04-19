using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildScript : MonoBehaviour
{
    GameObject connector;
    ConnectorScript connectorScript;
    public Material ConnectedMaterial;
    public Material DisconnectedMaterial;
    public Material BrokenMaterial;
    private ParticleSystem[] Particles;
    PartCollisionScript particleCollisionScript;
    private int currentTemp;
    private int maxTemp;
    float lerp;

    // Start is called before the first frame update
    void Start()
    {
        Particles = GetComponentsInChildren<ParticleSystem>();
        currentTemp = 0;
        maxTemp = 10000;
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
            connector = other.gameObject;
            connectorScript = connector.gameObject.GetComponent<ConnectorScript>();
            if (connectorScript.ConnectorActiveState == true)
            {
                this.gameObject.GetComponent<Renderer>().material = ConnectedMaterial;
                foreach (ParticleSystem particle in Particles)
                {
                    particle.Play();
                }
            }
            else
            {
                this.gameObject.GetComponent<Renderer>().material = DisconnectedMaterial;
                foreach (ParticleSystem particle in Particles)
                {
                    particle.Stop();
                }
            }
        }
    }

    void OnParticleCollision(GameObject other)
    {
        var script = other.gameObject.GetComponent<PartCollisionScript>();
        if (currentTemp < maxTemp)
        {
            currentTemp = currentTemp + script.numCollisionEvents;
            lerp = lerp + 0.05f * Time.deltaTime;
            this.gameObject.GetComponent<Renderer>().material.Lerp(DisconnectedMaterial, ConnectedMaterial, lerp);
        }
        else
        {
            lerp = lerp + 0.1f * Time.deltaTime;
            this.gameObject.GetComponent<Renderer>().material.Lerp(ConnectedMaterial, BrokenMaterial, lerp);
            Debug.Log("You burned the eggo");
        }
    }
}
