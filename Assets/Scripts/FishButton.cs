using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class FishButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject infoUI;
    private GameObject myObject;
    public const float timeToShow = 0.5f;
    private float timeFlow;
    private bool isOver;
    public int Index { private get; set; } = -1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Index > -1 && isOver)
        {
            timeFlow += Time.deltaTime;
            if(timeFlow >= timeToShow)
            {
                if(!myObject)
                {
                    myObject = Instantiate(infoUI, gameObject.transform);
                    myObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText(Player.Instance.FishTank[Index].Name);
                    myObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText(Player.Instance.FishTank[Index].Size.ToString() + " cm");
                    myObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().SetText(Player.Instance.FishTank[Index].Price.ToString());

                    float w1 = ((RectTransform)transform).rect.width;
                    float h1 = ((RectTransform)transform).rect.height;
                    float w2 = ((RectTransform)myObject.transform).rect.width;
                    float h2 = ((RectTransform)myObject.transform).rect.height;

                    myObject.transform.localScale = new Vector2(w1 / w2, h1 / h2);
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData e)
    {
        timeFlow = 0;
        isOver = true;
    }

    public void OnPointerExit(PointerEventData e)
    {
        if(myObject)
        {
            GameObject.Destroy(myObject);
        }
        
        timeFlow = 0;
        isOver = false;
        myObject = null;
    }
}
