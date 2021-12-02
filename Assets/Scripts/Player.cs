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
    
    public Store store { get; set; }
    
    public Player()
    {
        Equip = new Dictionary<Etype, Equipment>();
        store = new Store();

        Money = 0;
        Bait = new int[4];
        CurrentBait = 0;
        FishTank = new List<Fish>();

        foreach(Etype type in Enum.GetValues(typeof(Etype)))
        {
            Equip[type] = store.Equipments[type][0]; // 시작 장비
        }
    }

    public void GetFish(Fish fish)
    {
        if(FishTank.Count != ((Ship)Equip[Etype.Ship]).Capacity)
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

    public void Buy(Etype type, int level)
    {
        Equipment toBuy = store.Equipments[type][level-1];

        if(!toBuy.IsQualified(Equip[type].Level, GetAverageLevel()))
        {
            Debug.Log("구매 자격을 만족하지 못함");
        }
        else if(Money < toBuy.Price)
        {
            Debug.Log("돈이 충분하지 못함");
        }
        else
        {
            Money -= toBuy.Price;
            Equip[type] = toBuy;
            Debug.Log("구매에 성공함");
        }
    }
    
    public int GetAverageLevel()
    {
        return (Equip[Etype.Rod].Level + Equip[Etype.Reel].Level + Equip[Etype.Line].Level) / 3;
    }
}