using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRoriginController : MonoBehaviour
{
    [SerializeField] private GameObject xrOriginMain;
    public bool xrOriginMainOn = false;

    [SerializeField] private Transform xrOriginOriginalLocation;
    public bool xrOriginUIOn = true;

    [SerializeField] private Transform xrOriginUIOriginalLocation;
    public bool xrOriginCineOn = false;

    [SerializeField] private GameObject[] RayInteractors;

    private void Awake()
    {
        TurnOnUIXR();
    }

    public void TurnOnMainXR()
    {
        xrOriginMain.GetComponent<Transform>().position = xrOriginOriginalLocation.position;
        xrOriginMain.GetComponent<Transform>().rotation = xrOriginOriginalLocation.rotation;
        RayInteractors[0].SetActive(false);
        RayInteractors[1].SetActive(false);

        xrOriginMainOn = true;
        xrOriginUIOn = false;
        xrOriginCineOn = false;
    }

    public void TurnOnUIXR()
    {
        xrOriginMain.GetComponent<Transform>().position = xrOriginUIOriginalLocation.position;
        xrOriginMain.GetComponent<Transform>().rotation = xrOriginUIOriginalLocation.rotation;
        RayInteractors[0].SetActive(true);
        RayInteractors[1].SetActive(true);

        xrOriginMainOn = false;
        xrOriginUIOn = true;
        xrOriginCineOn = false;
    }
}
