using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player
{
    public int Money { get; set; }
    public int[] Bait { get; set; }
    public int CurrentBait { get; set; } // enum 쓰면 좋을듯
    public Equipment Rod { get; set; }
    public Equipment Reel { get; set; }
    public Equipment Line { get; set; }
    public Equipment Ship { get; set; }
    public List<Fish> FishTank { get; set; }
    
    public Player()
    {
        this.Money = 0;
        this.Bait = new int[4];
        this.CurrentBait = 0;
        // + 초기 아이템 대입
        this.FishTank = new List<Fish>();
    }

    public int GetAverageLevel()
    {
        return (this.Rod.Level + this.Reel.Level + this.Ship.Level) / 3;
    }
}