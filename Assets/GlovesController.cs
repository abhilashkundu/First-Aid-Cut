using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlovesController : MonoBehaviour
{
    [SerializeField] private Material GlovesMaterial;

    [SerializeField] private SkinnedMeshRenderer LeftHand;
    [SerializeField] private SkinnedMeshRenderer RightHand;

    [Header("2nd Step")]
    [SerializeField] private MainGameController mainGameController;

    [SerializeField] private CottonGaugeController cottonGaugeController;

    [SerializeField] private SoundController SoundController;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("LeftController") || other.gameObject.CompareTag("RightController"))
        {
            mainGameController.ExpectedBool = true;

            LeftHand.material = GlovesMaterial;
            RightHand.material = GlovesMaterial;

            mainGameController.GlovesONtrue();

            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.GetComponent<Collider>().enabled = false;

            cottonGaugeController.EnableGrab();

            mainGameController.StartTimeCheck();

            SoundController.PlayStep_2();
        }
    }
}
