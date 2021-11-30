using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    public static FishingManager Instance;

    public Fish fish;

    public float TimeLimit { get; set; }
    public float SuccessAreaSize { get; set; }
    public float CursorUpPower { get; set; }
    public float CursorDownPower { get; set; } = 0.01f;
    public float CursorSpeedLimit { get; set; } = 1f;
    public float CursorSpeed { get; set; } = 0f;
    public float RemainTime { get; set; } // Used in UI ?
    public float CursorPosition { get; set; } = 0f; // Used in UI
    public float TimeToSuccess { get; set; } = 120f;
    public float TimeInSuccessArea { get; set; } = 0f; // Used in UI ?

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        // DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        fish = Fish.RandomGenerate(0);

        CalcTimeLimit();
        CalcSuccessAreaSize();
        CalcCursorUpPower();

        RemainTime = TimeLimit;
    }

    void Update()
    {
        CursorPosition += CursorSpeed;
        CursorSpeed -= CursorDownPower;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CursorSpeed += CursorUpPower;
        }

        if(CursorSpeed < -CursorSpeedLimit)
        {
            CursorSpeed = -CursorSpeedLimit;
        }
        if(CursorSpeed > CursorSpeedLimit)
        {
            CursorSpeed = CursorSpeedLimit;
        }
    }

    public void StartGame()
    {
        Player.Instance.UseCurrentBait();
    }

    private void CalcTimeLimit()
    {
        // 임시
        TimeLimit = Player.Instance.Equip[Etype.Line].Stat - fish.Dexterity;
    }

    private void CalcSuccessAreaSize()
    {
        int rodStat = Player.Instance.Equip[Etype.Rod].Stat;
        SuccessAreaSize = (rodStat - fish.Speed) / rodStat; // 임시
    }

    private void CalcCursorUpPower()
    {
        // 임시
        CursorUpPower = Player.Instance.Equip[Etype.Reel].Stat - fish.Strength;
    }
}