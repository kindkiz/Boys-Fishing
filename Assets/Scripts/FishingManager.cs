using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    public GameObject fishingUIManager;
    public Fish fish;

    public const float MIN_POS = 0f;
    public const float MAX_POS = 500f;
    public const float BLANK = 50f;
    public const float MIN_TIME_LIMIT = 5f;
    public const float MAX_TIME_LIMIT = 15f;
    public const float MIN_SUCCESS_AREA_SIZE = 20f;
    public const float MAX_SUCCESS_AREA_SIZE = 200f;
    public const float TIME_TO_SUCCESS = 3f;
    public readonly float[] MIN_UP_POWER = {60f, 100f, 150f};
    public readonly float[] MAX_UP_POWER = {250f, 300f, 350f};
    public readonly float[] SPEED_LIMIT = {300f, 400f, 500f};
    public readonly float[] DOWN_POWER = {600f, 1000f, 1500f};

    public float TimeLimit;
    public float SuccessAreaSize;
    public float CursorUpPower;
    public float CursorDownPower;
    public float CursorSpeedLimit;
    public float CursorSpeed;
    public float RemainTime;
    public float CursorPosition;
    public float TimeToSuccess;
    public float TimeInSuccessArea;
    private int nowDepth;

    void OnEnable()
    {
        Player.Instance.UseCurrentBait();
        nowDepth = 1; //Player.Instance.Depth;

        fish = Fish.RandomGenerate(nowDepth);

        Init();

        fishingUIManager.SetActive(true);
    }

    void OnDisable()
    {
        fishingUIManager.SetActive(false);
    }

    void Update()
    {
        AddPosition(CursorSpeed);
        SpeedDown(CursorDownPower);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SpeedUp(CursorUpPower);
        }
        if(CheckArea())
        {
            TimeInSuccessArea += Time.deltaTime;
            if(TimeInSuccessArea >= TimeToSuccess)
            {
                print("Success: You get " + fish.Name);
                gameObject.SetActive(false);
            }
        }
        else
        {
            RemainTime -= Time.deltaTime;
            TimeInSuccessArea = 0;
            if(RemainTime <= 0)
            {
                print("Fail: You miss " + fish.Name);
                gameObject.SetActive(false);
            }
        }
    }

    private float Calculate(float min, float max, int diff)
    {
        if(diff < -3) diff = -3;
        if(diff > 3) diff = 3;

        return (max - min) / 6f * diff + (max + min) / 2f;
    }

    private void Init()
    {
        CalcTimeLimit();
        CalcSuccessAreaSize();
        CalcCursorUpPower();
        RemainTime = TimeLimit;
        
        CursorSpeedLimit = SPEED_LIMIT[nowDepth];
        CursorDownPower = DOWN_POWER[nowDepth];
        
        TimeInSuccessArea = 0f;
        CursorSpeed = 0f;
        CursorPosition = 0f;
    }

    private bool CheckArea()
    {
        float top = MAX_POS - BLANK;
        float bottom = top - SuccessAreaSize;
        return CursorPosition <= top && CursorPosition >= bottom;
    }

    private void SpeedUp(float speed)
    {
        CursorSpeed += speed;
        if(CursorSpeed > CursorSpeedLimit)
        {
            CursorSpeed = CursorSpeedLimit;
        }
    }

    private void SpeedDown(float speed)
    {
        CursorSpeed -= speed * Time.deltaTime;
        if(CursorSpeed < -CursorSpeedLimit)
        {
            CursorSpeed = -CursorSpeedLimit;
        }
    }

    private void AddPosition(float dist)
    {
        CursorPosition += dist * Time.deltaTime;
        if(CursorPosition > MAX_POS)
        {
            CursorPosition = MAX_POS;
            CursorSpeed = 0f;
        }
        if(CursorPosition < MIN_POS)
        {
            CursorPosition = MIN_POS;
            CursorSpeed = 0f;
        }
    }

    private void CalcTimeLimit()
    {
        int diff = Player.Instance.Equip[Etype.Line].Level - (int)fish.Dexterity;
        TimeLimit = Calculate(MIN_TIME_LIMIT, MAX_TIME_LIMIT, diff);
    }

    private void CalcSuccessAreaSize()
    {
        int diff = Player.Instance.Equip[Etype.Rod].Level - (int)fish.Speed;
        TimeLimit = Calculate(MIN_SUCCESS_AREA_SIZE, MAX_SUCCESS_AREA_SIZE, diff);
    }

    private void CalcCursorUpPower()
    {
        int diff = Player.Instance.Equip[Etype.Reel].Level - (int)fish.Strength;
        TimeLimit = Calculate(MIN_UP_POWER[nowDepth], MAX_UP_POWER[nowDepth], diff);
    }
}