using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreUIManager : MonoBehaviour
{
    public readonly int[] baitPrice = {5, 20, 20, 100};

    public GameObject equipTab;
    public GameObject baitTab;
    public GameObject equipPanel;
    public GameObject baitPanel;

    public TextMeshProUGUI totalPrice;

    public GameObject[] equipBtn;
    public GameObject[] baitBtn;

    private Equipment[] equipment;
    private bool isEquipTab;
    private int selectedIndex;
    private int priceSum;
    private int[] baitCounts;

    public void OnClickExit()
    {
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        equipTab.GetComponent<Button>().Select();
        OnClickEquipTab();
    }

    public void OnClickEquipTab()
    {
        isEquipTab = true;
        selectedIndex = -1;
        priceSum = 0;
        equipTab.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1);
        baitTab.GetComponent<Image>().color = new Color(0.8867f, 0.8867f, 0.8867f, 1);
        baitPanel.SetActive(false);
        equipPanel.SetActive(true);
        DisplayEquip();
    }

    public void OnClickBaitTab()
    {
        baitCounts = new int[4];
        for(int i = 0; i < 4; ++i)
        {
            baitBtn[i].GetComponent<BaitGoods>().InitCount();
        }
        isEquipTab = false;
        baitTab.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1);
        equipTab.GetComponent<Image>().color = new Color(0.8867f, 0.8867f, 0.8867f, 1);
        equipPanel.SetActive(false);
        baitPanel.SetActive(true);
        RefreshBaitUI();
    }

    public void DisplayEquip()
    {
        equipment = new Equipment[4];
        foreach(Etype type in Enum.GetValues(typeof(Etype)))
        {
            equipment[(int)type] = Store.Instance.GetEquipment(type, Player.Instance.Equip[type].Level + 1);
            equipBtn[(int)type].transform.Find("Name").GetComponent<TextMeshProUGUI>().text = equipment[(int)type].Name;
            equipBtn[(int)type].transform.Find("Img").GetComponent<Image>().sprite = equipment[(int)type].EqSprite;
            equipBtn[(int)type].transform.Find("Price").GetComponent<TextMeshProUGUI>().text = equipment[(int)type].Price.ToString();
        }
        RefreshEquipUI();
    }

    public void OnClickEquip(int index)
    {
        if(index != selectedIndex)
        {
            selectedIndex = index;
            priceSum = equipment[selectedIndex].Price;
        }
        else
        {
            selectedIndex = -1;
            priceSum = 0;
        }
        RefreshEquipUI();
    }

    public void OnClickBuy()
    {
        if(priceSum != 0)
        {
            bool success;
            if(isEquipTab)
            {
                success = Player.Instance.Buy(equipment[selectedIndex]);
                if(success)
                {
                    OnClickEquipTab();
                }
            }
            else
            {
                success = Player.Instance.Buy(baitCounts, priceSum);
                if(success)
                {
                    OnClickBaitTab();
                }
            }
        }
    }

    public void RefreshEquipUI()
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

        totalPrice.text = priceSum.ToString();
    }

    public void RefreshBaitUI()
    {
        priceSum = 0;
        for(int i = 0; i < 4; ++i)
        {
            baitCounts[i] = baitBtn[i].GetComponent<BaitGoods>().BaitCount;
            priceSum += baitPrice[i] * baitCounts[i];
        }

        totalPrice.text = priceSum.ToString();
    }
}
