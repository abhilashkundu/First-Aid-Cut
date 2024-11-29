using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsUpController : MonoBehaviour
{

    [SerializeField] private Animator HandsUpAnimation;
    [SerializeField] private GameObject Blood;
    [SerializeField] private CutItemController cutItemController;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("LeftController") || other.gameObject.CompareTag("RightController"))
        {
            HandsUpAnimation.SetTrigger("HandsUp");
            Blood.SetActive(false);
            GetComponent<Collider>().enabled = false;
            cutItemController.readyForStep4 = true;
        }
    }
}
