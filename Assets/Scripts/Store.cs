using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store
{
    public Dictionary<Etype, List<Equipment>> Equipments { get; }
    public Store()
    {
        string[] types = {"Rod", "Reel", "Line", "Ship"};
        
        for(int i = 0; i < types.Length; i++){
            Etype type = (Etype)i;
            List<Dictionary<string, object>> data = CSVReader.Read(types[i]);
            for(int j = 0; j < data.Count; j++){
                if(type == Etype.Ship){
                    Equipments[type].Add(new Ship(type, data[j]));
                }
                else{
                    Equipments[type].Add(new Equipment(type, data[j]));
                }
            }
        }
    }
}