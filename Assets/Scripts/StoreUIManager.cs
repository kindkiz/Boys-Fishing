using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreUIManager : MonoBehaviour
{
    public GameObject[] equipBtn;

    private Equipment[] equipment;
    private int selectedIndex;


    public void OnClickExit()
    {
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        selectedIndex = -1;
        DisplayGoods();
    }

    public void DisplayGoods()
    {
        equipment = new Equipment[4];
        foreach(Etype type in Enum.GetValues(typeof(Etype)))
        {
            equipment[(int)type] = Store.Instance.GetEquipment(type, Player.Instance.Equip[type].Level + 1);
            equipBtn[(int)type].transform.Find("Name").GetComponent<TextMeshProUGUI>().text = equipment[(int)type].Name;
            equipBtn[(int)type].transform.Find("Img").GetComponent<Image>().sprite = equipment[(int)type].EqSprite;
            equipBtn[(int)type].transform.Find("Price").GetComponent<TextMeshProUGUI>().text = equipment[(int)type].Price.ToString();
        }
        RefreshUI();
    }

    public void OnClickGoods(int index)
    {
        if(index != selectedIndex)
        {
            selectedIndex = index;
        }
        else
        {
            selectedIndex = -1;
        }
        RefreshUI();
    }

    public void OnClickBuy()
    {
        if(selectedIndex != -1)
        {
            bool success = Player.Instance.Buy(equipment[selectedIndex]);
            if(success)
            {
                OnEnable();
            }
        }
    }

    public void RefreshUI()
    {
        foreach(Etype type in Enum.GetValues(typeof(Etype)))
        {
            if((int)type == selectedIndex)
            {
                equipBtn[(int)type].GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                equipBtn[(int)type].GetComponent<Image>().color = new Color(0.8867f, 0.8867f, 0.8867f, 1);
            }
        }
    }
}
