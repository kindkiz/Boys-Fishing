using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store
{
    public Dictionary<Etype, List<Equipment>> Equipments { get; }
    public Store()
    {
        string[] typeName = {"Rod", "Reel", "Line", "Ship"};
        
        foreach(Etype type in Enum.GetValues(typeof(Etype)))
        {
            Equipments[type] = new List<Equipment>();
            List<Dictionary<string, object>> data = CSVReader.Read(typeName[(int)type]);
            for(int i = 0; i < data.Count; i++){
                if(type == Etype.Ship){
                    Equipments[type].Add(new Ship(type, data[i]));
                }
                else{
                    Equipments[type].Add(new Equipment(type, data[i]));
                }
            }
        }
    }
}