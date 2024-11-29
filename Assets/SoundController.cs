using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource HelpPerson;
    [SerializeField] private AudioSource TwoChanceThenAudio;
    [SerializeField] private AudioSource Congrats;
    [SerializeField] private AudioSource GetFirstAidBox;

    [SerializeField] private AudioSource Step_1_2ndAttempt;
    [SerializeField] private AudioSource Step_2_2ndAttempt;
    [SerializeField] private AudioSource Step_3_2ndAttempt;
    [SerializeField] private AudioSource Step_4_2ndAttempt;
    [SerializeField] private AudioSource Step_5_2ndAttempt;

    [SerializeField] private AudioSource NextStep;
    [SerializeField] private AudioSource Step_1stAttempt_Wrong;

    [SerializeField] private BlankFirstAidController blankFirstAidBoxCobntroller;

    [Header("1st Step")]
    [SerializeField] private MainGameController mainGameController;

    public void HelpPersonCall()
    {
        HelpPerson.Play();
        StartCoroutine(PlayNextAfterHelpPerson());
    }

    private IEnumerator PlayNextAfterHelpPerson()
    {
        while (HelpPerson.isPlaying)
        {
            yield return null;
        }

        GetFirstAidBox.Play();

        while (GetFirstAidBox.isPlaying)
        {
            yield return null;
        }

        blankFirstAidBoxCobntroller.ShowTheTransparentFirstAidBox();

        //First Time Check
        mainGameController.StartTimeCheck();

        PlayStep_1();
    }

    public void PlayTwoChanceThenAudio()
    {
        TwoChanceThenAudio.Play();
    }

    public void PlayCongrats()
    {
        Congrats.Play();
    }

    public void PlayGetFirstAidBox()
    {
        GetFirstAidBox.Play();
    }

    public void PlayStep_1()
    {
        Step_1_2ndAttempt.Play();
    }

    public void PlayStep_2()
    {
        Step_2_2ndAttempt.Play();
    }

    public void PlayStep_3()
    {
        Step_3_2ndAttempt.Play();
    }

    public void PlayStep_4()
    {
        Step_4_2ndAttempt.Play();
    }

    public void PlayStep_5()
    {
        Step_5_2ndAttempt.Play();
    }

    public void PlayNextStep()
    {
        NextStep.Play();
    }

    public void PlayStep_1stAttempt_Wrong()
    {
        Step_1stAttempt_Wrong.Play();
    }
}

