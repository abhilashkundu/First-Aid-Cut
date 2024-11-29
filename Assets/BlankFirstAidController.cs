using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankFirstAidController : MonoBehaviour
{
    [SerializeField] private GameObject FirstaidMoveable;
    [SerializeField] private GameObject FirstAidUseable;
    [SerializeField] private GameObject[] TransparentFirstAidBoxMaterial;

    [Header("All Items Inside First Aid Box")]
    [SerializeField] private GameObject GlovesInBox;

    [Header("First Aid Box Lid Animator")]
    [SerializeField] private Animator FirstAidOpenBox;

    [SerializeField] private Animator cutPlayerAnimator;

    [SerializeField] private MainGameController MainGameController;

    private void Awake()
    {
        for (int i = 0; i < 2; i++)
        {
            TransparentFirstAidBoxMaterial[i].SetActive(false);
        }
    }

    public void ShowTheTransparentFirstAidBox()
    {
        for(int i = 0; i < 2; i++)
        {
            TransparentFirstAidBoxMaterial[i].SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("FirstAidTrigger"))
        {

            MainGameController.SetExpectedBoolTrue();

            cutPlayerAnimator.SetTrigger("HandLeave");

            FirstaidMoveable.SetActive(false);

            FirstAidUseable.SetActive(true);

            GlovesInBox.SetActive(true);

            FirstAidOpenBox.SetTrigger("LidOpen");

            Destroy(this.gameObject);
        }
    }
}
