using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store
{
    public Dictionary<Equipment.Type, List<Equipment>> Equipments { get; }
    public Store()
    {
        string[] types = {"Rod", "Reel", "Line", "Ship"};
        
        for(int i = 0; i < types.Length; i++){
            Equipment.Type type = (Equipment.Type)i;
            List<Dictionary<string, object>> data = CSVReader.Read(types[i]);
            for(int j = 0; j < data.Count; j++){
                Equipments[type].Add(new Equipment(type, data[j]));
            }
        }
    }
}