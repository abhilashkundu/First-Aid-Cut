using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameController : MonoBehaviour
{
    public bool GlovesON = false;
    public bool ExpectedBool = false;
    [SerializeField] private Animator CutAnim;
    [SerializeField] private SoundController soundController;

    [SerializeField] private GameObject[] stepVideo;
    [SerializeField] private int[] Steps;
    int i = 0;

    public int score = 0;

    bool timerRunning = false;

    [SerializeField] TextMeshProUGUI score_Text;

    private void Awake()
    {
        score_Text.text = "Score : " + score;
    }

    public void GlovesONtrue()
    {
        GlovesON = true;
    }

    public void StartGame()
    {
        CutAnim.SetTrigger("CutApple");
    }

    public void StartTimeCheck()
    {
        for (int j = 0; j < 5; j++)
        {
             stepVideo[j].SetActive(false);
        }

        ExpectedBool = false;
        StartCoroutine(CheckExpectedBoolCoroutine());
    }

    public void IncreaseScore()
    {
        score += 25;
        score_Text.text = "Score : " + score;
    }

    public void Success()
    {
        Steps[i] = 1;
        i += 1;

        IncreaseScore();
    }

    public void Failure()
    {
        soundController.PlayStep_1stAttempt_Wrong();

        timerRunning = false;

        if (i == 0)
        {
            for(int j = 0; j < 5; j++)
            {
                if (j != i)
                {
                    stepVideo[i].SetActive(false);
                }

                stepVideo[i].SetActive(true);
            }
        }
        else if (i == 1)
        {
            for (int j = 0; j < 5; j++)
            {
                if (j != i)
                {
                    stepVideo[i].SetActive(false);
                }

                stepVideo[i].SetActive(true);
            }
        }
        else if (i == 2)
        {
            for (int j = 0; j < 5; j++)
            {
                if (j != i)
                {
                    stepVideo[i].SetActive(false);
                }

                stepVideo[i].SetActive(true);
            }
        }
        else if (i == 3)
        {
            for (int j = 0; j < 5; j++)
            {
                if (j != i)
                {
                    stepVideo[i].SetActive(false);
                }
            }
        }
        else if (i == 4)
        {
            for (int j = 0; j < 5; j++)
            {
                if (j != i)
                {
                    stepVideo[i].SetActive(false);
                }

                stepVideo[i].SetActive(true);
            }
        }

        Steps[i] = 1;
        i += 1;
    }
    
    private IEnumerator CheckExpectedBoolCoroutine()
    {
        float timer = 0f;
        while (timer < 15f)
        {
            timerRunning = true;

            if (ExpectedBool)
            {
                Success();
                yield break;
            }
            timer += Time.deltaTime;
            yield return null;
        }

        Failure();
    }

    public void SetExpectedBoolTrue()
    {
        ExpectedBool = true;
    }


    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
