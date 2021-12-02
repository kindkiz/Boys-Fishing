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

        Equip[Etype.Rod] = store.Equipments[Etype.Rod][0];
        Equip[Etype.Reel] = store.Equipments[Etype.Reel][0];
        Equip[Etype.Line] = store.Equipments[Etype.Line][0];
        Equip[Etype.Ship] = store.Equipments[Etype.Ship][0];
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
        return (float)FishTank.Count / Equip[Etype.Ship].Stat;
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