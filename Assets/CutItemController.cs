using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class CutItemController : MonoBehaviour
{

    [Header("3rd and 4th Step")]
    [SerializeField] private MainGameController mainGameController;
    bool cottonGaudgeDone = false;
    bool secondCottonGaudgeDone = false;

    [SerializeField] bool TimeForGrip = false;
    [SerializeField] bool GripDone = false;

    [SerializeField] private GameObject bloodiedCottonGaudge;
    [SerializeField] private GameObject secondBloodiedCottonGaudge;

    [SerializeField] private Transform secondGaugeLocation;

    [SerializeField] private GameObject GripGand;

    [SerializeField] private Transform leftHandTransform;  // Reference to player's left hand transform
    [SerializeField] private Transform rightHandTransform; // Reference to player's right hand transform

    [SerializeField] private SkinnedMeshRenderer leftHandMesh;  // SkinnedMeshRenderer for the left hand
    [SerializeField] private SkinnedMeshRenderer rightHandMesh; // SkinnedMeshRenderer for the right hand

    [SerializeField] private GameObject HandsUpCollider;

    [SerializeField] private GameObject bandageRoll;
    [SerializeField] private GameObject bandageRollWheel;

    private float gripDistanceThreshold = 0.05f;
    public bool readyForStep4 = false;

    private float gripTimer = 0f;
    private bool gripHandActive = false;  // To track if grip hand has been active for 3 seconds
    private float gripDurationThreshold = 3f; // The time in seconds required to trigger the event

    bool rollDone = false;

    [SerializeField] XRKnob bandageRollKnob;
    [SerializeField] GameObject rolledBandageInArm;

    [SerializeField] Collider pressurePointCollider;
    [SerializeField] private SoundController soundController;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CottonGauge") && !cottonGaudgeDone && mainGameController.GlovesON)
        {
            cottonGaudgeDone = true;
            GameObject gO = other.gameObject;
            gO.SetActive(false);
            gO.GetComponent<Transform>().position = secondGaugeLocation.position;
            gO.GetComponent<Transform>().rotation = secondGaugeLocation.rotation;
            gO.SetActive(true);
            bloodiedCottonGaudge.SetActive(true);
            TimeForGrip = true;
        }
        if (other.gameObject.CompareTag("CottonGauge") && cottonGaudgeDone && !secondCottonGaudgeDone && mainGameController.GlovesON && readyForStep4)
        {
            secondCottonGaudgeDone = true;
            other.gameObject.SetActive(false);
            secondBloodiedCottonGaudge.SetActive(true);
            TimeForGrip = true;
        }

        if(other.gameObject.CompareTag("roll") && !rollDone)
        {
            rollDone = true;
            bandageRollWheel.SetActive(true);
            other.gameObject.SetActive(false);
            mainGameController.SetExpectedBoolTrue();
        }
    }

    bool once = false;

    private void Update()
    {
        if(rollDone && bandageRollKnob.temp_value >= 0.9f && !once)
        {
            once = true;
            rolledBandageInArm.SetActive(true);
            bandageRollWheel.SetActive(false);
            pressurePointCollider.enabled = true;
            mainGameController.StartTimeCheck();
            soundController.PlayStep_5();
        }
        if (TimeForGrip)
        {
            // Check distance to left and right hands
            float distanceToLeftHand = Vector3.Distance(transform.position, leftHandTransform.position);
            float distanceToRightHand = Vector3.Distance(transform.position, rightHandTransform.position);

            // Check if either hand is close enough for gripping
            bool isLeftHandNear = distanceToLeftHand < gripDistanceThreshold;
            bool isRightHandNear = distanceToRightHand < gripDistanceThreshold;

            // Activate/Deactivate GripGand based on proximity
            GripGand.SetActive(isLeftHandNear || isRightHandNear);

            // Toggle visibility of left hand mesh
            leftHandMesh.enabled = !isLeftHandNear;

            // Toggle visibility of right hand mesh
            rightHandMesh.enabled = !isRightHandNear;

            GripDone = true;

            // Check if the grip hand is active
            if (GripGand.activeSelf)
            {
                // Start or continue the timer
                gripTimer += Time.deltaTime;

                // If the grip hand is active for more than 3 seconds
                if (gripTimer >= gripDurationThreshold && !gripHandActive)
                {
                    gripHandActive = true;
                    OnGripHandActiveFor3Seconds();  // Trigger the event once after 3 seconds
                }
            }
            else
            {
                // Reset the timer and the active state if the grip hand is not active
                gripTimer = 0f;
                gripHandActive = false;
            }
        }
    }

    int chance = 1;
    // Function to be called after grip hand is active for 3 seconds
    private void OnGripHandActiveFor3Seconds()
    {
        if(chance == 1)
        {
            mainGameController.SetExpectedBoolTrue();
            soundController.PlayStep_3();
            ResetHands();
            chance += 1;
            mainGameController.StartTimeCheck();
            return;
        }

        if(chance == 2)
        {
            mainGameController.SetExpectedBoolTrue();

            soundController.PlayStep_4();

            mainGameController.SetExpectedBoolTrue();
            GripGand.SetActive(false);

            // Enable original left and right hand meshes
            leftHandMesh.enabled = true;
            rightHandMesh.enabled = true;

            // Reset grip state
            GripDone = false;
            TimeForGrip = false;

            // Reset the timer and active state
            gripTimer = 0f;
            gripHandActive = false;

            chance += 1;

            bandageRoll.SetActive(true);

            mainGameController.StartTimeCheck();

            return;
        }
    }

    public void ResetHands()
    {
        // Ensure GripGand is deactivated
        GripGand.SetActive(false);

        // Enable original left and right hand meshes
        leftHandMesh.enabled = true;
        rightHandMesh.enabled = true;

        // Reset grip state
        GripDone = false;
        TimeForGrip = false;

        HandsUpCollider.SetActive(true);

        // Reset the timer and active state
        gripTimer = 0f;
        gripHandActive = false;
    }
}