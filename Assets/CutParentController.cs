using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CutParentController : MonoBehaviour
{
    [SerializeField] private GameObject Apple;
    [SerializeField] private GameObject Knife;

    [SerializeField] private VisualEffect bloodVFX;

    // Adjust the force multiplier to control the strength of the applied force
    [SerializeField] private float forceMultiplier = 5f;

    [SerializeField] private SoundController soundController;
    [SerializeField] private MainGameController MainGameController;

    private void Awake()
    {
        bloodVFX.Stop();
    }

    public void ReleaseAppleAndKnife()
    {
        // Enable gravity and collider
        Rigidbody appleRb = Apple.GetComponent<Rigidbody>();
        Rigidbody knifeRb = Knife.GetComponent<Rigidbody>();

        appleRb.useGravity = true;
        knifeRb.useGravity = true;

        Apple.GetComponent<Collider>().enabled = true;
        Knife.GetComponent<Collider>().enabled = true;

        // Detach them from their parent
        Apple.transform.parent = null;
        Knife.transform.parent = null;

        // Apply random force in any direction
        Vector3 randomForceApple = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ) * forceMultiplier;

        Vector3 randomForceKnife = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ) * forceMultiplier;

        appleRb.AddForce(randomForceApple, ForceMode.Impulse);
        knifeRb.AddForce(randomForceKnife, ForceMode.Impulse);
    }

    public void BloodSplash()
    {
        bloodVFX.Play();
        soundController.HelpPersonCall();
    }

    public void HandsUp()
    {
        MainGameController.StartTimeCheck();
    }
}
