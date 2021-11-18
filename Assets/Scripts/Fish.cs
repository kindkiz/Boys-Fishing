using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fish
{
    public int Price { get; set; }
    public float Size { get; set; }
    public string Name { get; set; }

    // 얼마나 빨리 도망가는지 (시간 제한)
    public float Dexterity { get; set; }    
    // 얼마나 빨리 눌러야하는지
    public float Strength { get; set; } 
    // 얼마나 바가 작은지
    public float Speed { get; set; } 

    public Fish()
    {
        Price = 0;
        Size = 0;
        Name = "";
        Dexterity = 0;
        Strength = 0;
        Speed = 0;
    }

    public Image GetImage()
    {
        // 이미지 정보 불러와서 반환
        return null;
    }
}
