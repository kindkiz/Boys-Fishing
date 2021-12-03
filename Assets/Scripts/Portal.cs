using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string nextSceneName;
    public int shipLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == GameManager.TAG_PLAYER)
        {
            if(((Ship)Player.Instance.Equip[Etype.Ship]).Level >= shipLevel)
            {
            SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.Log("배의 레벨이 부족합니다");
            }
        }
    }
}
