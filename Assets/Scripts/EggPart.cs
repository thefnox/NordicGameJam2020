﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggPart : MonoBehaviour, IReceivesHeat
{
    public float temperature = 25;
    public int health = 100;
    
    public void emitHeat()
    {
        throw new System.NotImplementedException();
    }

    public float getTemperature()
    {
        throw new System.NotImplementedException();
    }

    public float getThermalConductivity()
    {
        throw new NotImplementedException();
    }

    public void receiveHeat(float value, float distance)
    {
        var intensity = value / (4f * Math.PI * Mathf.Pow(distance, 2f));

    }
}
