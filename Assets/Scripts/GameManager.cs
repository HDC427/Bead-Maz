using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager theGM;
    public TextMeshProUGUI counterText, timeText;
    [SerializeField] private GameObject titleScreen;
    private int timeRemaining;
    public static bool started, paused;
    // Start is called before the first frame update
    void Start()
    {
        theGM = this;
        started = false;
        paused = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!started)
            {
                GameStart();
            }
            else
            {
                GamePause();
            }
        }
    }

    void GameStart()
    {
        started = true;
        paused = false;
        SpawnManager.theSM.GameStart();
        titleScreen.SetActive(false);
        timeRemaining = 60;
        InvokeRepeating(nameof(UpdateTime), 0, 1);
    }

    void GamePause()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1;
        }
        else
        {
            paused = true;
            Time.timeScale = 0;
        }
    }

    public void UpdateCount(int delta)
    {
        SpawnManager.numBeadsRemaining += delta;
        counterText.text = SpawnManager.numBeadsRemaining + "/" + SpawnManager.numBeadsTotal;
    }

    void UpdateTime()
    {
        timeText.text = timeRemaining + "s";
        timeRemaining--;
    }
}
