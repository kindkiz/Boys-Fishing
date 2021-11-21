using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishMarket
{
    public FishMarket()
    {

    }

    public void SellFish(Fish fish)
    {
        Player.Instance.Money += fish.Price;
        Player.Instance.FishTank.Remove(fish);
        DrawMarket();
    }

    public void DrawMarket()
    {
        // 상점 그릴 정보들 정리 후 넘겨줌
    }
}
