using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store
{
    public List<Equipment> Rods { get; }
    public List<Equipment> Reels { get; }
    public List<Equipment> Lines { get; }
    public List<Ship> Ships { get; }
    public Store()
    {
        Rods = new List<Equipment>();
        Reels = new List<Equipment>();
        Lines = new List<Equipment>();
        Ships = new List<Ship>();

        List<Dictionary<string, object>> rodData = CSVReader.Read("Rod");
        for(int i = 0; i < rodData.Count; i++){
            Rods.Add(new Equipment(Equipment.Type.Rod, rodData[i]));
        }

        List<Dictionary<string, object>> reelData = CSVReader.Read("Reel");
        for(int i = 0; i < reelData.Count; i++){
            Reels.Add(new Equipment(Equipment.Type.Reel, reelData[i]));
        }

        List<Dictionary<string, object>> lineData = CSVReader.Read("Line");
        for(int i = 0; i < lineData.Count; i++){
            Lines.Add(new Equipment(Equipment.Type.Line, lineData[i]));
        }

        List<Dictionary<string, object>> shipData = CSVReader.Read("Ship");
        for(int i = 0; i < shipData.Count; i++){
            Ships.Add(new Ship(Equipment.Type.Ship, shipData[i]));
        }
    }
}