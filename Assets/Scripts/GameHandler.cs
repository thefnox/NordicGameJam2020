using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandler : IGameService
{
    public ObjectsWithPrefab[] objects;
    public int money;
    public int startMoney;
    public bool playMode = false;
    public List<SerializedSelectable> state = new List<SerializedSelectable>();

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public int GetCurrentMoney()
    {
        return money;
    }

    public GameObject LoadState()
    {
        var list = new List<SelectableObject>();
        var parent = new GameObject("ItemContainer");

        foreach (SerializedSelectable serialized in state)
        {
            var obj = objects.First(o => o.name == serialized.name);
            if (obj.prefab == null)
            {
                throw new System.Exception($"Object {serialized.name} not found in GameHandler dictionary!");
            }

            var gameObj = Object.Instantiate(
                obj.prefab,
                new Vector3(serialized.position.x, serialized.position.y, serialized.position.z),
                new Quaternion(serialized.rotation.x, serialized.rotation.y, serialized.rotation.z, serialized.rotation.w),
                parent.transform
            );
            var selectable = gameObj.GetComponent<SelectableObject>();
            selectable.PlaceObject();
            list.Add(selectable);
        }

        return parent;
    }

    public void Reset()
    {
        money = startMoney;
    }

    public void SaveState(SelectableObject[] objs)
    {
        state.Clear();
        foreach (SelectableObject obj in objs)
        {
            state.Add(new SerializedSelectable(obj));
        }
    }

    public bool IsInPlay()
    {
        return playMode;
    }

    public void TogglePlayMode(bool toggle)
    {
        playMode = toggle;
    }

    public void EditMode()
    {
        TogglePlayMode(false);
        Initiate.Fade("PlayTest", Color.black, 0.5f);
    }

    public void PlayMode()
    {
        TogglePlayMode(true);
        Initiate.Fade("PlayTest", Color.black, 0.5f);
    }

    public void StartGame()
    {
        Reset();
        EditMode();
    }

    public void WinGame()
    {
        TogglePlayMode(true);
        SceneManager.LoadScene("WinScene", LoadSceneMode.Single);
    }

    public GameHandler(ObjectsWithPrefab[] objects, int startMoney)
    {
        this.objects = objects;
        this.startMoney = startMoney;
        this.money = startMoney;
    }
}
