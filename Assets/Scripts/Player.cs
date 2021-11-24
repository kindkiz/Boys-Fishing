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
    
    public Player()
    {
        this.Money = 0;
        this.Bait = new int[4];
        this.CurrentBait = 0;
        this.FishTank = new List<Fish>();

        Buy(Etype.Rod, 1);
        Buy(Etype.Reel, 1);
        Buy(Etype.Line, 1);
        Buy(Etype.Ship, 1);
    }

    public float GetCapacityRatio()
    {
        return (float)this.FishTank.Count / this.Equip[Etype.Ship].Stat;
    }

    public void Buy(Etype type, int level)
    {
        Store store = new Store();
        Equipment toBuy = store.Equipments[type][level-1];
        if(!toBuy.IsQualified())
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
    
    public int GetAverageLevel()
    {
        return (this.Equip[Etype.Rod].Level + this.Equip[Etype.Reel].Level + this.Equip[Etype.Line].Level) / 3;
    }
}