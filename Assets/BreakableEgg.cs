using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableEgg : MonoBehaviour
{

    public float breakForce = 1f;


    public bool broken = false;
    public bool overrideGame = false;
    public GameObject shellBottom;
    public GameObject shellTop;
    public GameObject egg;

    public void Start()
    {
        if (!ServiceLocator.Resolve<IGameService>().IsInPlay() && !overrideGame)
        {
            GetComponent<Collider>().enabled = false;
            var rigidBody = GetComponent<Rigidbody>();
            rigidBody.useGravity = false;
            rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude > breakForce)
        {
            BreakEgg(collision.impulse.magnitude);
        }
    }

    public void BreakEgg(float magnitude)
    {
        if (broken || (!ServiceLocator.Resolve<IGameService>().IsInPlay() && !overrideGame))
        {
            return;
        }

        Debug.Log($"Egg broke with force {magnitude}");
        var top = Instantiate(shellTop);
        top.transform.position = transform.position;
        top.transform.rotation = transform.rotation;
        var bottom = Instantiate(shellBottom);
        bottom.transform.position = transform.position;
        bottom.transform.rotation = transform.rotation;
        var egg = Instantiate(this.egg);
        egg.transform.position = transform.position;
        egg.transform.rotation = transform.rotation;

        broken = true;
        Destroy(gameObject);
    }
}
