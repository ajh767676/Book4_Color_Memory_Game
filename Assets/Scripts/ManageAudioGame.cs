using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ManageAudioGame : MonoBehaviour
{
    private const int STATE_PLAY_SEQUENCE = 1;
    private const int STATE_WAIT_FOR_USER_INPUT = 2;
    private const int STATE_PROCESS_USER_INPUT = 3;
    private const int WINNING_SEQUENCE_LENGTH = 5;

    private int currentState;
    int colorSubmitted;
    int numberOfColors;

    int[] sequenceOfColor = new int[100];
    int[] sequenceOfColorsSubmitted = new int[100];

    public void submitColor(int newColor)
    {
        playNote(newColor);

        print("You have pressed color " + newColor);

        if (currentState == STATE_WAIT_FOR_USER_INPUT)
        {
            colorSubmitted = newColor;

            sequenceOfColorsSubmitted[nbColorsSubmitted] = newColor;
            nbColorsSubmitted++;

            bool rightMove = assessUserCurrentMove();

            if (!rightMove)
            {
                loadLoseLevel();
                return;
            }

            if (nbColorsSubmitted == indexOfColor)
            {
                currentState = STATE_PROCESS_USER_INPUT;
            }
        }
    }

    int indexOfColor = 0;

    bool newColorHasBeenGenerated, allBoxesDisplayed;
    bool startTimer, waitingTimerActivated, waitingTimeElapsed;

    float timer, waitingTime, timeDelayBetweenDisplays;

    int animationIndex, nbColorsSubmitted;

    void Start()
    {
        currentState = STATE_PLAY_SEQUENCE;
        sequenceOfColor = new int[100];

        newColorHasBeenGenerated = false;

        startTimer = false;
        waitingTimerActivated = false;
        waitingTimeElapsed = false;
        allBoxesDisplayed = false;

        timer = 0;
        waitingTime = 0;
        animationIndex = 0;
        timeDelayBetweenDisplays = 2;
        nbColorsSubmitted = 0;

        //hideBoxes();
        //displayBox(1);

        //testing
        //sequenceOfColor = new int[] { 1, 2, 3, 4, 2 };
        //indexOfColor = 4;

        numberOfColors = PlayerPrefs.GetInt("NumberOfColors", 4);
        setupColors();

        updateUI();
    }

   

    void Update()
    {
        switch (currentState)
        {
            case STATE_PLAY_SEQUENCE:

                if (!newColorHasBeenGenerated)
                {
                    initTimer();
                    generateNewColor();
                    newColorHasBeenGenerated = true;
                    allBoxesDisplayed = false;
                    hideBoxes();
                }

                timer += Time.deltaTime;

                if (startTimer &&
                    timer >= timeDelayBetweenDisplays &&
                    !waitingTimerActivated)
                {
                    timer = 0;

                    hideBoxes();
                    displayBox(sequenceOfColor[animationIndex]);
                    animationIndex++;

                    if (animationIndex >= indexOfColor)
                    {
                        startTimer = false;
                        animationIndex = 0;
                        waitingTimerActivated = true;
                    }
                }

                if (waitingTimerActivated)
                {
                    waitingTime += Time.deltaTime;

                    if (waitingTime >= timeDelayBetweenDisplays)
                    {
                        waitingTime = 0;
                        hideBoxes();
                        waitingTimerActivated = false;
                        waitingTimeElapsed = true;
                    }
                }

                if (waitingTimeElapsed)
                {
                    currentState = STATE_WAIT_FOR_USER_INPUT;
                    nbColorsSubmitted = 0;
                }

                break;

            case STATE_WAIT_FOR_USER_INPUT:

                if (!allBoxesDisplayed)
                {
                    setBoxColors();
                    allBoxesDisplayed = true;
                }

                break;

            case STATE_PROCESS_USER_INPUT:

                bool okResult = assessUserMove();

                if (okResult)
                {
                    if (indexOfColor >= WINNING_SEQUENCE_LENGTH)
                    {
                        FindAnyObjectByType<GameController>()
                            .CompleteGame();
                    }
                    else
                    {
                        animationIndex = 0;
                        newColorHasBeenGenerated = false;
                        timer = 0;
                        waitingTimerActivated = false;
                        waitingTimeElapsed = false;
                        currentState = STATE_PLAY_SEQUENCE;
                    }
                }
                else
                {
                    loadLoseLevel();
                }

                break;

            default:
                break;
        }
    }

    void initTimer()
    {
        startTimer = true;
        timer = 0;
        animationIndex = 0;
    }

    public bool assessUserMove()
    {
        bool allPerfect = true;

        for (int i = 0; i < indexOfColor; i++)
        {
            int correctColor = sequenceOfColor[i];
            int submittedColor = sequenceOfColorsSubmitted[i];

            if (correctColor != submittedColor)
            {
                allPerfect = false;
            }
        }

        if (allPerfect)
        {
            print("WELL DONE!");
            updateUI();
            return true;
        }
        else
        {
            print("NOT RIGHT!");
            return false;
        }
    }

    public bool assessUserCurrentMove()
    {
        if (sequenceOfColorsSubmitted[nbColorsSubmitted - 1] ==
            sequenceOfColor[nbColorsSubmitted - 1])
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void generateNewColor()
    {
        int r = Random.Range(1, numberOfColors + 1);
        sequenceOfColor[indexOfColor] = r;
        indexOfColor++;
    }

    public void loadLoseLevel()
    {
        PlayerPrefs.SetInt("score", indexOfColor - 1);
        SceneManager.LoadScene("chapter2_lose");
    }

    public void updateUI()
    {
        GameObject.Find("nbMemorized")
            .GetComponent<TMP_Text>().text = "" + indexOfColor;
    }

    void hideBoxes()
    {
        for (int i = 1; i <= numberOfColors; i++)
        {
            GameObject.FindWithTag("" + i)
                .GetComponent<Image>().enabled = false;
        }
    }

    void setBoxColors()
    {
        for (int i = 1; i <= numberOfColors; i++)
        {
            GameObject.FindWithTag("" + i)
                .GetComponent<Image>().enabled = true;
        }
    }

    void setupColors()
    {
        for (int i = 1; i <= 4; i++)
        {
            GameObject box = GameObject.FindWithTag("" + i);
            box.SetActive(i <= numberOfColors);
        }
    }

    public void displayBox(int index)
    {
        GameObject.FindWithTag("" + index)
            .GetComponent<Image>().enabled = true;

        playNote(index);
    }

    public void playNote(int index)
    {
        float pitch = 0.5f;
        float note = (float)index;

        GetComponent<AudioSource>().pitch =
            pitch * Mathf.Pow(2.0f, note / 12.0f);

        GetComponent<AudioSource>().Play();
    }
}