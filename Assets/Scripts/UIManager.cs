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
        fillGage.GetComponent<Image>().fillAmount = Player.Instance.FishTank.Count/((Ship)Player.Instance.Equip[Etype.Ship]).Capacity;
    }

    public void EquipBait(Sprite selectedBait) {
        
        bait.GetComponent<Image>().sprite = selectedBait;
        bait.transform.GetComponentInChildren<TextMeshProUGUI>().text = Player.Instance.Bait[Player.Instance.CurrentBait].ToString();
    }
}
