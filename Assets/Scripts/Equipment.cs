using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment
{
    public enum Type { Rod, Reel, Line, Ship }

    public string Name { get; }
    public Type EquipmentType { get; }
    public int Level { get; }
    public int Stat { get; }
    public int Price { get; }

    public Equipment() {}
    public Equipment(Type type, Dictionary<string, object> data)
    {
        this.Name = "없앨까"; // or 낚싯대 I, 낚싯대 IV 이런식으로 자동 생성 or 낡은 낚싯대, 최고급 낚싯대 이런식으로 이름붙일까
        this.EquipmentType = type;
        this.Level = (int)data["Level"];
        this.Stat = (int)data["Stat"];
        this.Price = (int)data["Price"];
    }

    public bool IsQualified(Player player)
    {
        switch(EquipmentType)
        {
            case Type.Rod:
                return player.Rod.Level + 1 == this.Level;
            case Type.Reel:
                return player.Reel.Level + 1 == this.Level;
            case Type.Line:
                return player.Line.Level + 1 == this.Level;
            case Type.Ship:
                return player.Ship.Level + 1 == this.Level && player.GetAverageLevel() >= this.Level;
            default:
                return false;
        }
    }
}

public class Ship : Equipment
{
    public int MaxHp { get; }
    public int Hp { get; set; }
    public int RepairCostPerHp { get; }

    public Ship(Type type, Dictionary<string, object> data) : base(Type.Ship, data)
    {
        this.MaxHp = (int)data["Hp"];
        this.Hp = this.MaxHp;
        this.RepairCostPerHp = (int)data["RepairCost"];
    }
}