using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public bool IsMarket { get; set; }
    public bool IsObstacle { get; set; }
    public bool IsStore { get; set; }
    private List<GameObject> depthList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        IsMarket = false;
        IsObstacle = false;
        IsStore = false;
    }

    // Update is called once per frame
    void Update()
    {
        SetDepth();
    }

    void OnTriggerEnter(Collider collider)
    {
        switch(collider.gameObject.tag)
        {
            case GameManager.TAG_MARKET:
                IsMarket = true;
                break;
            case GameManager.TAG_TERRAIN:
                IsObstacle = true;
                break;
            case GameManager.TAG_STORE:
                IsStore = true;
                break;
            case GameManager.TAG_DEPTH:
                if(!depthList.Contains(collider.gameObject))
                {
                    depthList.Add(collider.gameObject);
                }
                break;
            default:
                break;

        }
    }

    void OnTriggerExit(Collider collider)
    {
        switch(collider.gameObject.tag)
        {
            case GameManager.TAG_MARKET:
                IsMarket = false;
                break;
            case GameManager.TAG_TERRAIN:
                IsObstacle = false;
                break;
            case GameManager.TAG_STORE:
                IsStore = false;
                break;
            case GameManager.TAG_DEPTH:
                depthList.Remove(collider.gameObject);
                break;
            default:
                break;

        }
    }

    void SetDepth()
    {
        int depth = 0;
        foreach(GameObject i in depthList)
        {
            if(i.GetComponent<Depth>())
            {
                int idepth = i.GetComponent<Depth>().depth;
                if(idepth > depth)
                    depth = idepth;
            }
        }

        if(Player.Instance.Depth != depth)
        {
            Player.Instance.Depth = depth;
            Debug.Log("깊이 " + depth + " 진입!");
        }
    }
}
