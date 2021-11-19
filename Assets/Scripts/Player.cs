using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player
{
    public int Money { get; set; }
    public int[] Bait { get; set; }
    public int CurrentBait { get; set; }
    public Dictionary<Equipment.Type, Equipment> Equip { get; set; }
    public List<Fish> FishTank { get; set; }
    
    public Player()
    {
        this.Money = 0;
        this.Bait = new int[4];
        this.CurrentBait = 0;
        this.FishTank = new List<Fish>();

        Buy(Equipment.Type.Rod, 1);
        Buy(Equipment.Type.Reel, 1);
        Buy(Equipment.Type.Line, 1);
        Buy(Equipment.Type.Ship, 1);
    }

    public void Buy(Equipment.Type type, int level)
    {
        Store store = new Store();
        Equipment toBuy = store.Equipments[type][level-1];
        if(!toBuy.IsQualified(this)){
            Debug.Log("구매 자격을 만족하지 못함");
        }
        else if(this.Money < toBuy.Price){
            Debug.Log("돈이 충분하지 못함");
        }
        else{
            this.Money -= toBuy.Price;
            this.Equip[type] = toBuy;
            Debug.Log("구매에 성공함");
        }
        // 자격, 돈 검사를 여기서 하는게 맞나?
    }
    
    public int GetAverageLevel()
    {
        return (this.Equip[Equipment.Type.Rod].Level + this.Equip[Equipment.Type.Reel].Level + this.Equip[Equipment.Type.Line].Level) / 3;
    }
}