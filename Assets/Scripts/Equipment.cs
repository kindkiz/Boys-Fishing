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
    public Equipment(string name, Type type, int level, int stat, int price)
    {
        this.Name = name;
        this.EquipmentType = type;
        this.Level = level;
        this.Stat = stat;
        this.Price = price;
    }

    bool IsQualified(Player player)
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

    public Ship(string name, Type type, int level, int stat, int price, int maxHp, int repairCostPerHp) : base(name, type, level, stat, price)
    {
        this.MaxHp = maxHp;
        this.Hp = maxHp;
        this.RepairCostPerHp = repairCostPerHp;
    }
}