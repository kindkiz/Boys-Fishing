using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    [SerializeField] GameObject bait;
    [SerializeField] GameObject coin;
    [SerializeField] GameObject fillGage;
    [SerializeField] Image hp;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        bait.transform.GetComponentInChildren<TextMeshProUGUI>().text = Player.Instance.Bait[Player.Instance.CurrentBait].ToString();
        coin.transform.GetComponentInChildren<TextMeshProUGUI>().text = Player.Instance.Money.ToString();

        // 현재 물고기 갯수 / 배용량으로 인벤토리 게이지 설정
        fillGage.GetComponent<Image>().fillAmount = Player.Instance.GetCapacityRatio();
        if(Player.Instance.IsFull())
        {
            fillGage.GetComponent<Image>().color = new Color(1f, 0.5344f, 0.1839f, 1);
        }
        else
        {
            fillGage.GetComponent<Image>().color = new Color(0.3118f, 0.7680f, 0.8584f, 1);
        }

        float hpRatio = Player.Instance.GetHpRatio();
        hp.fillAmount = hpRatio;
        if(hpRatio < 0.1)
        {
            hp.color = Color.red;
        }
        else if(hpRatio < 0.5)
        {
            hp.color = Color.yellow;
        }
        else{
            hp.color = Color.green;
        }
    }

    public void EquipBait(Sprite selectedBait) {
        
        bait.GetComponent<Image>().sprite = selectedBait;
        bait.transform.GetComponentInChildren<TextMeshProUGUI>().text = Player.Instance.Bait[Player.Instance.CurrentBait].ToString();
    }
}
