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

    public void EquipBait(Sprite selectedBait) {
        
        bait.GetComponent<Image>().sprite = selectedBait;
        // TODO: 플레이어 current bait 바꾸기
        // 남은 미끼 갯수 보여주기
        bait.transform.GetComponentInChildren<TextMeshProUGUI>().text = Player.Instance.Bait[Player.Instance.CurrentBait].ToString();
    }
}
