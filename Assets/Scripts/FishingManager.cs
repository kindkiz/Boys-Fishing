using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    public static FishingManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        // DontDestroyOnLoad(this.gameObject);
    }
    public Fish fish;

    public const float MIN_POS = 0f;
    public const float MAX_POS = 500f;
    public const float BLANK = 50f;

    public float TimeLimit;
    public float SuccessAreaSize = 0.3f;
    public float CursorUpPower;
    public float CursorDownPower = 0.1f;
    public float CursorSpeedLimit = 2.5f;
    public float CursorSpeed = 0f;
    public float RemainTime; // Used in UI ?
    public float CursorPosition = 0f; // Used in UI
    public float TimeToSuccess = 120f;
    public float TimeInSuccessArea = 0f; // Used in UI ?

    void OnEnable()
    {
        //fish = Fish.RandomGenerate(0);
        fish = new Fish("test fish", 5f, 100, 1, 1, 1);

        CalcTimeLimit();
        CalcSuccessAreaSize();
        CalcCursorUpPower();

        RemainTime = TimeLimit;
    }

    void Update()
    {
        AddPosition(CursorSpeed);
        AddSpeed(-CursorDownPower);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AddSpeed(CursorUpPower);
        }
        if(CheckArea())
        {
            TimeInSuccessArea += 1;
        }
        else
        {
            RemainTime -= 1;
        }
    }

    private bool CheckArea()
    {
        float top = MAX_POS - BLANK;
        float bottom = top - SuccessAreaSize;
        return CursorPosition <= top && CursorPosition >= bottom;
    }

    private void AddSpeed(float speed)
    {
        CursorSpeed += speed;
        if(CursorSpeed > CursorSpeedLimit)
        {
            CursorSpeed = CursorSpeedLimit;
        }
        if(CursorSpeed < -CursorSpeedLimit)
        {
            CursorSpeed = -CursorSpeedLimit;
        }
    }

    private void AddPosition(float dist)
    {
        CursorPosition += dist;
        if(CursorPosition > MAX_POS)
        {
            CursorPosition = MAX_POS;
        }
        if(CursorPosition < MIN_POS)
        {
            CursorPosition = MIN_POS;
            CursorSpeed = 0f;
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

        SuccessAreaSize = 0.2f; //test
    }

    private void CalcCursorUpPower()
    {
        // 임시
        CursorUpPower = Player.Instance.Equip[Etype.Reel].Stat - fish.Strength;

        CursorUpPower = 1.9f; // test
    }
}