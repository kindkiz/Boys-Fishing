using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishMarket
{
    Player player;

    public FishMarket()
    {
        this.player = player;
    }

    public SellFish(Player player, Fish fish)
    {
        player.Money += fish.Price;
        player.FishTank.Remove(fish);
        DrawMarket(player);
    }

    public DrawMarket(Player player)
    {
        // 상점 그릴 정보들 정리 후 넘겨줌
    }
}
