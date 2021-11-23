using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager
{
    private float timeLimit;
    private float barSize;
    private float barForce;
    public FishingManager(Fish fish)
    {
        this.timeLimit = CalcTimeLimit(fish.Dexterity);
        this.barSize = CalcBarSize(fish.Dexterity);
        this.barForce = CalcBarForce(fish.Dexterity);
    }

    public void StartGame()
    {
        
    }

    private float CalcTimeLimit(float dexterity)
    {
        // 임시
        float result = Player.Instance.Equip[Etype.Line].Stat - dexterity;
        return result;
    }

    private float CalcBarSize(float speed)
    {
        // 임시
        float result = Player.Instance.Equip[Etype.Rod].Stat - speed;
        return result;
    }

    private float CalcBarForce(float strength)
    {
        // 임시
        float result = Player.Instance.Equip[Etype.Reel].Stat - strength;
        return result;
    }
}