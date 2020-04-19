using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IGameService 
{
    void Reset();

    bool IsInPlay();

    void TogglePlayMode(bool toggle);


    int GetCurrentMoney();

    void AddMoney(int amount);

    GameObject LoadState();

    void SaveState(SelectableObject[] objs);
}
