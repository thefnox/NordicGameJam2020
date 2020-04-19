using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoneyCounter : MonoBehaviour
{
    public int prevMoney = 0;

    void Update()
    {
        var text = GetComponent<Text>();
        var currentMoney = ServiceLocator.Resolve<IGameService>().GetCurrentMoney();
        var delta = Mathf.RoundToInt(1000f * Time.deltaTime);
        var money = 0;
        if (prevMoney <= currentMoney)
        {
            money = prevMoney + delta < currentMoney ? prevMoney + delta : currentMoney;
        } else
        {
            money = prevMoney - delta > currentMoney ? prevMoney - delta : currentMoney;
        }
        if (text != null)
        {
            text.text = $"Budget: ${money}";
        }
        prevMoney = money;
    }
}
