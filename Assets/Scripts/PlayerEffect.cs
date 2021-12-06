using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    GameObject waterEff;
    Animator playerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = this.GetComponent<Animator>();
        waterEff = this.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAnimator.GetBool("isMoving"))
        {
            waterEff.SetActive(true);
        }
        else
        {
            waterEff.SetActive(false);
        }
    }
}
