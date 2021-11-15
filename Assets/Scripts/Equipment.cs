using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipment
{
    enum Type { Rod, Reel, Line, Ship }

    public string Name { get; set; }
    public Type EquipmentType { get; set; }
    public int Level { get; set; }
    public int Stat { get; set; }
    public int Price { get; set; }

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
        }
    }
}