using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IGameService 
{
    void Reset();

    int GetCurrentMoney();

    void AddMoney(int amount);

    GameObject LoadState();

    void SaveState(SelectableObject[] objs);
}
