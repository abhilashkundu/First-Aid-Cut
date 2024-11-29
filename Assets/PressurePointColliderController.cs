using Keyboard;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class PressurePointColliderController : MonoBehaviour
{
    [SerializeField] private GameObject pressureHand;
    [SerializeField] private MainGameController MainGameController;
    [SerializeField] private SoundController soundController;

    [SerializeField] private GameObject scoreCard;
    [SerializeField] private GameObject mainSceneAssets;
    [SerializeField] private HighscoreTable highscoreTable;
    [SerializeField] private KeyboardManager keyboardManager;
    [SerializeField] private MainGameController mainGameController;
    [SerializeField] private DynamicMoveProvider dynamicMoveProvider;
    [SerializeField] private Transform XRoriginLastPos;
    [SerializeField] private Transform XRoriginOriginal;

    [SerializeField] private GameObject leftXrRay;
    [SerializeField] private GameObject rightXrRay;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("LeftController") || other.gameObject.CompareTag("RightController"))
        {
            pressureHand.SetActive(true);
            MainGameController.SetExpectedBoolTrue();
            soundController.PlayCongrats();
            GetComponent<Collider>().enabled = false;
            highscoreTable.AddHighscoreEntry(mainGameController.score, keyboardManager.playerName);
            StartCoroutine(delayInLastScene());
        }
    }

    IEnumerator delayInLastScene()
    {
        yield return new WaitForSeconds(5f);
        leftXrRay.SetActive(true);
        rightXrRay.SetActive(true);

        XRoriginOriginal.position = XRoriginLastPos.position;
        XRoriginOriginal.rotation = XRoriginLastPos.rotation;
        dynamicMoveProvider.useGravity = false;
        scoreCard.SetActive(true);
        mainSceneAssets.SetActive(false);
    }
}