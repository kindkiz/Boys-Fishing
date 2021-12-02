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
    public int Price { get; }
    public Sprite EqSprite { get; }

    public Equipment() {}
    public Equipment(Etype type, Dictionary<string, object> data)
    {
        Type = type;
        Level = (int)data["Level"];
        Price = (int)data["Price"];
        Name = Naming();
        EqSprite = GetSprite();
    }

    public string Naming()
    {
        string[] typeName = {"낚싯대", "릴", "낚싯줄", "배"};
        string[] levelName = {"0", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX"};
        return typeName[(int)Type] + " " + levelName[Level];
    }

    public bool IsQualified(int nowLevel, int avgLevel)
    {
        bool ret = nowLevel + 1 == Level; // 직전 레벨의 장비를 구매했는지
        if(Type == Etype.Ship)
        {
            ret &= (avgLevel >= Level); // 배를 제외한 다른 장비들의 평균 레벨이 해당 레벨에 도달했는지
        }
        return ret;
    }

    private Sprite GetSprite()
    {
        string[] typeName = {"Rod", "Reel", "Line", "Ship"};
        string filePath = typeName[(int)Type] + "Img/" + typeName[(int)Type] + Level.ToString();
        return Resources.Load<Sprite>(filePath);
    }
}

public class Ship : Equipment
{
    public int Capacity { get; }
    public float MaxHp { get; }
    public float Hp { get; set; }
    public int RepairCostPerHp { get; }
    public bool IsDead { get; set; }

    public Ship(Etype type, Dictionary<string, object> data) : base(Etype.Ship, data)
    {
        Capacity = (int)data["Capacity"];
        MaxHp = (int)data["Hp"];
        Hp = MaxHp;
        RepairCostPerHp = (int)data["RepairCost"];
        IsDead = false;
    }

    public void Repair()
    {
        Hp = MaxHp;
    }

    public void WearOut(float hp)
    {
        Hp -= hp;
        if(Hp < 0)
        {
            Hp = 0;
            IsDead = true;
        }
    }
}