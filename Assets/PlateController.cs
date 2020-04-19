using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{
    public PlateTrigger trigger;
    public bool won = false;

    public void CheckWin()
    {
        if (won) return;
        if (trigger.eggParts.Count >= 10)
        {
            foreach(var egg in trigger.eggParts)
            {
                if (egg.temperature < 60 || egg.temperature > 100 || egg.health < 25)
                {
                    return;
                }
            }
            won = true;
        }
    }

    private void Update()
    {
        CheckWin();
    }
}
