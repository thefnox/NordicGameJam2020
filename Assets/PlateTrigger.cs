using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateTrigger : MonoBehaviour
{
    private List<GameObject> colliders = new List<GameObject>();
    public List<EggPart> eggParts = new List<EggPart>();

    private void OnTriggerEnter(Collider other)
    {

        if (!colliders.Contains(other.gameObject))
        {

            colliders.Add(other.gameObject);
            if (other.gameObject.layer == LayerMask.NameToLayer("Egg"))
            {
                var eggPart = other.gameObject.GetComponent<EggPart>();
                if (eggPart != null && !eggParts.Contains(eggPart))
                {
                    eggParts.Add(eggPart);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (colliders.Contains(other.gameObject))
        {
            var eggPart = other.gameObject.GetComponent<EggPart>();
            if (eggPart != null && eggParts.Contains(eggPart))
            {
                eggParts.Remove(eggPart);
            }
            colliders.Remove(other.gameObject);

        }
    }
}
