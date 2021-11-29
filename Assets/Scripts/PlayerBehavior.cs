using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public bool IsMarket { get; set; }
    public bool IsObstacle { get; set; }
    public bool IsStore { get; set; }
    public bool IsPortal { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        IsMarket = false;
        IsObstacle = false;
        IsPortal = false;
        IsStore = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        switch(collider.gameObject.tag)
        {
            case GameManager.TAG_MARKET:
                IsMarket = true;
                break;
            case GameManager.TAG_PORTAL:
                IsPortal = true;
                break;
            case GameManager.TAG_TERRAIN:
                IsObstacle = true;
                break;
            case GameManager.TAG_STORE:
                IsStore = true;
                break;
            default:
                Debug.Log("알 수 없는 태그와 충돌 : " + collider.gameObject.tag);
                break;

        }
        Debug.Log("Enter : " + collider.gameObject.tag);
    }

    void OnTriggerExit(Collider collider)
    {
        switch(collider.gameObject.tag)
        {
            case GameManager.TAG_MARKET:
                IsMarket = false;
                break;
            case GameManager.TAG_PORTAL:
                IsPortal = false;
                break;
            case GameManager.TAG_TERRAIN:
                IsObstacle = false;
                break;
            case GameManager.TAG_STORE:
                IsStore = false;
                break;
            default:
                Debug.Log("알 수 없는 태그와 충돌 : " + "collider.gameObject.tag");
                break;

        }
                Debug.Log("Exit : " + collider.gameObject.tag);
    }
}
