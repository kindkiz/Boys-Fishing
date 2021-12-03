using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if(instance == null){
                instance = new Player();
            }
            return instance;
        }
    }

    public int Depth { get; set; }
    public int Money { get; set; }
    public int[] Bait { get; set; }
    public int CurrentBait { get; set; }
    public Dictionary<Etype, Equipment> Equip { get; set; }
    public List<Fish> FishTank { get; set; }
    public List<Fish> SelectedFish { get; set; }
    
    public Player()
    {
        Equip = new Dictionary<Etype, Equipment>();

        Money = 10000;
        Bait = new int[] {10, 0, 0, 1};
        CurrentBait = 0;
        FishTank = new List<Fish>();

        foreach(Etype type in Enum.GetValues(typeof(Etype)))
        {
            Equip[type] = Store.Instance.GetEquipment(type, 1); // 시작 장비
        }
    }

    public bool IsFull()
    {
        return FishTank.Count == ((Ship)Equip[Etype.Ship]).Capacity;
    }

    public void GetFish(Fish fish)
    {
        if(!IsFull())
        {
            FishTank.Add(fish);
        }
        else
        {
            Debug.Log("Player: 용량 초과");
        }
    }

    public bool HasCurrentBait()
    {
        return Bait[CurrentBait] >= 1;
    }

    public void UseCurrentBait()
    {
        Bait[CurrentBait] -= 1;
    }

    public float GetCapacityRatio()
    {
        return (float)FishTank.Count / ((Ship)Equip[Etype.Ship]).Capacity;
    }

    public bool Buy(Equipment toBuy)
    {
        if(!toBuy.IsQualified(Equip[toBuy.Type].Level, GetAverageLevel()))
        {
            Debug.Log("구매 자격을 만족하지 못함");
            return false;
        }
        else if(Money < toBuy.Price)
        {
            Debug.Log("돈이 충분하지 못함");
            return false;
        }
        else
        {
            Money -= toBuy.Price;
            Equip[toBuy.Type] = toBuy;
            Debug.Log("구매에 성공함");
            return true;
        }
    }

    public bool Buy(int[] baitCounts, int price)
    {
        if(Money < price)
        {
            Debug.Log("돈이 충분하지 못함");
            return false;
        }
        else
        {
            Money -= price;
            for(int i = 0; i < 4; ++i)
            {
                Bait[i] += baitCounts[i];
            }
            Debug.Log("구매에 성공함");
            return true;
        }
    }
    
    public int GetAverageLevel()
    {
        return (Equip[Etype.Rod].Level + Equip[Etype.Reel].Level + Equip[Etype.Line].Level) / 3;
    }
}