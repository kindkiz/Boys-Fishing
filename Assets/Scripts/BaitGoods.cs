using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaitGoods : MonoBehaviour
{
    public const int MAX_CNT = 99;

    public Button plusButton;
    public Button minusButton;
    public TextMeshProUGUI countText;

    public int BaitCount { get; private set; }

    void OnEnable()
    {
        BaitCount = 0;
        refreshUI();
    }

    public void refreshUI()
    {
        countText.text = BaitCount.ToString();
    }

    public void OnClickPlus()
    {
        if(BaitCount < MAX_CNT)
        {
            BaitCount++;
            refreshUI();
        }
    }

    public void OnClickMinus()
    {
        if(BaitCount > 0)
        {
            BaitCount--;
            refreshUI();
        }
    }

    public void InitCount()
    {
        BaitCount = 0;
        refreshUI();
    }
}
