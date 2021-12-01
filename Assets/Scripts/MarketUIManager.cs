using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarketUIManager : MonoBehaviour
{
    public GameObject content;
    public GameObject fishButton;
    public GameObject totalPriceText;

    private bool[] isSelect;
    private int totalPrice;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickExit()
    {
        gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        SetFishList();

    }

    public void SetFishList()
    {
        if(!content)
        {
            Debug.Log("Market: Can't find Content");
            return;
        }
        if(!fishButton)
        {
            Debug.Log("Market: Can't find Fish Button");
            return;
        }

        foreach(Transform child in content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        List<Fish> fishTank = Player.Instance.FishTank;
        isSelect = new bool[fishTank.Count];
        RefreshTotalPrice();
        
        int idx = 0;
        foreach(Fish fish in fishTank)
        {
            if(fish == null)
            {
                Debug.Log("Market : There is a 'Null' in Fish Tank");
                continue;
            }
            
            GameObject btn = Instantiate(fishButton);

            btn.transform.SetParent(content.transform);
            btn.transform.localScale = new Vector3(1, 1, 1);

            if(fish.Image != null)
            {
                btn.transform.Find("FishImg").GetComponent<Image>().sprite = fish.Image;
            }
            btn.transform.Find("FishTMP").GetComponent<TextMeshProUGUI>().text = fish.Name;


            Debug.Log(idx);
            int i = idx;
            btn.GetComponent<Button>().onClick.AddListener(delegate{OnItemClick(btn, i);});

            isSelect[idx] = false;
            idx++;
        }
    }

    public void OnItemClick(GameObject button, int idx)
    {
        Debug.Log(isSelect);
        Debug.Log(idx);
        isSelect[idx] = !isSelect[idx];

        if(isSelect[idx])
        {
            button.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            button.GetComponent<Image>().color = new Color(0.7843f, 0.7843f, 0.7843f, 1);
        }

        RefreshTotalPrice();
    }

    public void RefreshTotalPrice()
    {
        List<Fish> fishTank = Player.Instance.FishTank;
        totalPrice = 0;
        for(int i = 0; i < fishTank.Count; i++)
        {
            if(isSelect[i])
            {
                totalPrice += fishTank[i].Price;
            }
        }

        if(totalPriceText)
        {
            totalPriceText.GetComponent<TextMeshProUGUI>().text = totalPrice.ToString();
        }
    }
}
