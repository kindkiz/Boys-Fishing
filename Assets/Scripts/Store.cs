using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store
{
    private static Store instance;
    public static Store Instance
    {
        get
        {
            if(instance == null){
                instance = new Store();
            }
            return instance;
        }
    }
    public void Restart()
    {
        instance = new Store();
    }
    public Dictionary<Etype, List<Equipment>> Equipments { get; }
    public Store()
    {
        Equipments = new Dictionary<Etype, List<Equipment>>();
        string[] typeName = {"Rod", "Reel", "Line", "Ship"};
        
        foreach(Etype type in Enum.GetValues(typeof(Etype)))
        {
            Equipments[type] = new List<Equipment>();
            List<Dictionary<string, object>> data = CSVReader.Read(typeName[(int)type]);
            for(int i = 0; i < data.Count; i++)
            {
                if(type == Etype.Ship)
                {
                    Equipments[type].Add(new Ship(type, data[i]));
                }
                else
                {
                    Equipments[type].Add(new Equipment(type, data[i]));
                }
            }
        }
    }

    public Equipment GetEquipment(Etype type, int level)
    {
        return Equipments[type][level-1];
    }

}