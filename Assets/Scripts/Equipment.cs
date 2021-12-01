using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Etype { Rod, Reel, Line, Ship }

public class Equipment
{
    public string Name { get; }
    public Etype Type { get; }
    public int Level { get; }
    public int Stat { get; }
    public int Price { get; }

    public Equipment() {}
    public Equipment(Etype type, Dictionary<string, object> data)
    {
        this.Type = type;
        this.Level = (int)data["Level"];
        this.Stat = (int)data["Stat"];
        this.Price = (int)data["Price"];
        this.Name = Naming();
    }

    public string Naming()
    {
        string[] typeName = {"낚싯대", "릴", "낚싯줄", "배"};
        string[] levelName = {"0", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX"};
        return typeName[(int)Type] + " " + levelName[Level];
    }

    public bool IsQualified(int nowLevel, int avgLevel)
    {
        bool ret = nowLevel + 1 == this.Level; // 직전 레벨의 장비를 구매했는지
        if(this.Type == Etype.Ship)
        {
            ret &= (avgLevel >= this.Level); // 배를 제외한 다른 장비들의 평균 레벨이 해당 레벨에 도달했는지
        }
        return ret;
    }
}

public class Ship : Equipment
{
    public float MaxHp { get; }
    public float Hp { get; set; }
    public int RepairCostPerHp { get; }

    public Ship(Etype type, Dictionary<string, object> data) : base(Etype.Ship, data)
    {
        this.MaxHp = (int)data["Hp"];
        this.Hp = this.MaxHp;
        this.RepairCostPerHp = (int)data["RepairCost"];
    }

    public void Repair()
    {
        this.Hp = MaxHp;
    }

    public void WearOut(float hp)
    {
        this.Hp -= hp;
        if(this.Hp < 0)
        {
            this.Hp = 0;
        }
    }
}