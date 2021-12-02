using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UserInfoUIManager : MonoBehaviour
{
    [SerializeField] Transform infosParent;
    Transform[] infos;

    private void Awake()
    {
        int childCnt = infosParent.childCount;
        infos = new Transform[childCnt];
        for (int i = 0; i < childCnt; i++)
        {
            infos[i] = infosParent.GetChild(i);
        }  
    }

    public void OnClickUserInfo() {
        gameObject.SetActive(true);
        ChangeUserInfo();
    }

    public void OnClickExitBtn() {
        gameObject.SetActive(false);
    }

    private void ChangeUserInfo() {
        // rod, reel, line, ship -> img, level, name 바꾸기
        for (int i = 0; i < infos.Length - 1; i++) {
            Sprite img = Player.Instance.Equip[(Etype)i].EqSprite;
            string level = Player.Instance.Equip[(Etype)i].Level.ToString();
            string name = Player.Instance.Equip[(Etype)i].Name;
            ChangeInfo(infos[i], img, level, name);
        }

        // boy -> level 바꾸기
        infos[infos.Length - 1].GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = Player.Instance.GetAverageLevel().ToString();
              
    }

    private void ChangeInfo(Transform t, Sprite img, string level, string name) {
        t.GetComponent<Image>().sprite = img;
        t.GetChild(0).GetComponent<TextMeshProUGUI>().text = level;
        t.GetChild(1).GetComponent<TextMeshProUGUI>().text = name;
    }
}
