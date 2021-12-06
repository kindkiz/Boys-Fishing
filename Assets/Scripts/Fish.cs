using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct FishInfo{
    public string name;
    //public Texture2D image;
    public Sprite image;
    public int uniqueness;
    public float minimumSize;
    public float maximumSize;
    public int minimumPrice;
    public int maximumPrice;
    public int dexterity;
    public int strength;
    public int speed;
}

public class Fish
{
    // 물고기의 각 희귀 등급마다 얼마나 더 안나오게 할지
    private static float multiple = 0.33f;
    // 각 지역별 최대 깊이 (최소는 1로 가정)
    private const int maxDepth = 2;
    private const int minDepth = 0;
    // 물고기 크기 소수점 (digit) 자리까지 표시
    private const int digit = 1;
    // 미끼를 쓰지 않았을 시 크기 감소량
    private const float noBaitSize = 0.7f;

    public static List<FishInfo> FishList { get; set; }
    public string Name { get; set; }
    public float Size { get; set; }
    public int Price { get; set; }
    public Sprite Image { get; set; }

    // 얼마나 빨리 도망가는지 (시간 제한)
    public float Dexterity { get; set; }    
    // 얼마나 빨리 눌러야하는지
    public float Strength { get; set; } 
    // 얼마나 바가 작은지
    public float Speed { get; set; } 

    public Fish()
    {
        Name = "";
        Size = 0;
        Price = 0;
        Dexterity = 0;
        Strength = 0;
        Speed = 0;
    }

    public Fish(string name, float size, int price, int dexterity, int strength, int speed)
    {
        this.Name = name;
        this.Size = size;
        this.Price = price;
        this.Dexterity = dexterity;
        this.Strength = strength;
        this.Speed = speed;
    }

    public Fish(string name, float size, int price, int dexterity, int strength, int speed, Sprite image)
    {
        this.Name = name;
        this.Size = size;
        this.Price = price;
        this.Dexterity = dexterity;
        this.Strength = strength;
        this.Speed = speed;
        this.Image = image;
    }

    public Image GetImage()
    {
        // 이미지 정보 불러와서 반환
        return null;
    }

    public static Fish RandomGenerate(int depth, int bait)
    {
        if(depth < minDepth || depth > maxDepth)
        {
            Debug.Log("RandomGerate: 잘못 된 깊이 값 입력");
            return null;
        }

        int fishIndex = RandomIndex(depth);

        if(fishIndex > -1)
        {
            FishInfo fish = FishList[fishIndex];

            float correction = noBaitSize;
            if(bait == GameManager.BAIT_BLUE || bait == GameManager.BAIT_STRONG)
            {
                correction = 1.0f;
            }

            float size = RandomSize(fish.minimumSize, fish.maximumSize, correction);
            int price = GetPrice(size, fish.minimumSize, fish.maximumSize, fish.minimumPrice, fish.maximumPrice);

            return new Fish(fish.name, size, price, fish.dexterity, fish.strength, fish.speed, fish.image);
        }
        else
        {
            Debug.Log("RandomGerate: 인덱스 에러");
            return null;
        }

        return null;
    }

    private static int RandomIndex(int depth)
    {
        int output = -1;
        float sum = 0;
        List<float> weight = new List<float>();

        foreach(FishInfo fish in FishList)
        {
            float w = Mathf.Pow(multiple, Mathf.Abs(fish.uniqueness - depth - 1));
            weight.Add(w);
            sum += w;
        }

        for(int i = 0; i < FishList.Count; i++)
        {
            if(i == FishList.Count - 1 || Random.Range(0.0f, sum) <= weight[i])
            {
                return i;
            }
            sum -= weight[i];
        }

        return output;
    }

    private static float RandomSize(float minSize, float maxSize, float correction)
    {
        return Mathf.Round(Random.Range(minSize, (maxSize - minSize) * correction + minSize) * Mathf.Pow(10, digit)) / Mathf.Pow(10, digit);
    }

    private static int GetPrice(float size, float minSize, float maxSize, int minPrice, int maxPrice)
    {
        if(maxSize == 0)
        {
            return minPrice;
        }
        return Mathf.RoundToInt(((size - minSize) / (maxSize - minSize)) * (maxPrice - minPrice) + minPrice);
    }
}
