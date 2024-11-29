using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CottonGaugeController : MonoBehaviour
{
    public void EnableGrab()
    {
        GetComponent<Collider>().enabled = true;
    }
}
