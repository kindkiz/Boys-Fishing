using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketUIManager : MonoBehaviour
{
    public GameObject content;
    public GameObject fishButton;

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
        foreach(Fish fish in fishTank)
        {
            GameObject btn = Instantiate(fishButton);

            btn.transform.SetParent(content.transform);
            btn.transform.localScale = new Vector3(1, 1, 1);
            btn.transform.Find("FishImg").GetComponent<Image>().sprite = fish.Image;

            Debug.Log(btn);
        }
    }
}
