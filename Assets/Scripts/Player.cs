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

        this.Money = 0;
        this.Bait = new int[4];
        this.CurrentBait = 0;
        this.FishTank = new List<Fish>();

        this.Equip[Etype.Rod] = store.Equipments[Etype.Rod][0];
        this.Equip[Etype.Reel] = store.Equipments[Etype.Reel][0];
        this.Equip[Etype.Line] = store.Equipments[Etype.Line][0];
        this.Equip[Etype.Ship] = store.Equipments[Etype.Ship][0];
    }

    public void UseCurrentBait()
    {
        Bait[CurrentBait] -= 1;
    }

    public float GetCapacityRatio()
    {
        return (float)this.FishTank.Count / this.Equip[Etype.Ship].Stat;
    }

    public void Buy(Etype type, int level)
    {
        Equipment toBuy = store.Equipments[type][level-1];

        if(!toBuy.IsQualified(Equip[type].Level, GetAverageLevel()))
        {
            Debug.Log("구매 자격을 만족하지 못함");
        }
        else if(this.Money < toBuy.Price)
        {
            Debug.Log("돈이 충분하지 못함");
        }
        else
        {
            this.Money -= toBuy.Price;
            this.Equip[type] = toBuy;
            Debug.Log("구매에 성공함");
        }
    }

    public void Select(Fish fish)
    {
        if(this.SelectedFish.Contains(fish))
        {
            this.SelectedFish.Remove(fish);
        }
        else{
            this.SelectedFish.Add(fish);
        }
    }

    public void SelectAll()
    {
        this.SelectedFish = this.FishTank;
    }

    public void Sell()
    {
        foreach(Fish fish in this.SelectedFish)
        {
            this.Money += fish.Price;
            this.SelectedFish.Remove(fish);
            this.FishTank.Remove(fish);
        }
    }
    
    private int GetAverageLevel()
    {
        return (this.Equip[Etype.Rod].Level + this.Equip[Etype.Reel].Level + this.Equip[Etype.Line].Level) / 3;
    }
}