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

    public float TimeLimit;
    public float SuccessAreaSize;
    public float CursorUpPower;
    public float CursorDownPower;
    public float CursorSpeedLimit;
    public float CursorSpeed;
    public float RemainTime;
    public float CursorPosition;
    public float TimeToSuccess = 3f;
    public float TimeInSuccessArea;

    void OnEnable()
    {
        Player.Instance.UseCurrentBait();

        fish = Fish.RandomGenerate(1);

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

    private void Init()
    {
        CalcTimeLimit();
        CalcSuccessAreaSize();
        CalcCursorUpPower();
        RemainTime = TimeLimit;
        TimeInSuccessArea = 0f;
        CursorSpeed = 0f;
        CursorPosition = 0f;

        //CursorDownPower = 600f;
        //CursorSpeedLimit = 300f;
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
        // 임시
        TimeLimit = Player.Instance.Equip[Etype.Line].Level - fish.Dexterity;

        TimeLimit = 20f;
    }

    private void CalcSuccessAreaSize()
    {
        //int levelDiff = Player.Instance.Equip[Etype.Rod].Level - fish.Speed;
        //SuccessAreaSize = (rodStat - fish.Speed) / rodStat; // 임시

        SuccessAreaSize = 50f; //test
    }

    private void CalcCursorUpPower()
    {
        // 임시
        CursorUpPower = Player.Instance.Equip[Etype.Reel].Level - fish.Strength;

        CursorUpPower = 100f; // test
    }
}