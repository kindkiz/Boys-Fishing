using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager
{
    public float TimeLimit { get; }
    public float SuccessAreaSize { get; }
    public float CursorUpPower { get; }
    public float CursorDownPower { get; } = 0.01f;
    public float CursorSpeedLimit { get; } = -1f;
    public float SuccessAreaStart { get; } // Used in UI
    public float SuccessAreaEnd { get; } = 90f; // Used in UI
    public float RemainTime { get; } // Used in UI ?
    public float CursorPosition { get; } = 0f; // Used in UI
    public float TimeToSuccess { get; } = 120f;
    public float TimeInSuccessArea { get; } = 0f; // Used in UI ?


    public FishingManager(Fish fish)
    {
        TimeLimit = CalcTimeLimit(fish.Dexterity);
        SuccessAreaSize = CalcSuccessAreaSize(fish.Speed);
        CursorUpPower = CalcCursorUpPower(fish.Strength);

        SuccessAreaStart = SuccessAreaEnd - SuccessAreaSize;
        RemainTime = TimeLimit;
    }

    public void StartGame()
    {
        Player.Instance.Bait[Player.Instance.CurrentBait] -= 1;
        
    }

    private float CalcTimeLimit(float dexterity)
    {
        // 임시
        float result = Player.Instance.Equip[Etype.Line].Stat - dexterity;
        return result;
    }

    private float CalcSuccessAreaSize(float speed)
    {
        // 임시
        float result = Player.Instance.Equip[Etype.Rod].Stat - speed;
        return result;
    }

    private float CalcCursorUpPower(float strength)
    {
        // 임시
        float result = Player.Instance.Equip[Etype.Reel].Stat - strength;
        return result;
    }
}