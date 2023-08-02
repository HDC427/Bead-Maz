using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager theGM;
    public TextMeshProUGUI counterText, timeText;
    private int timeRemaining;
    // Start is called before the first frame update
    void Start()
    {
        theGM = this;
        timeRemaining = 60;
        InvokeRepeating(nameof(UpdateTime), 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
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
