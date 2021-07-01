using UnityEngine.UI;
using UnityEngine;

public class ScoreMonitor : MonoBehaviour
{
    public IntVariable marioScore;
    public Text text;

    public void Start()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        text.text = "Score: " + marioScore.Value.ToString();
    }

    void OnApplicationQuit(){
	    marioScore.SetValue(0);
    }   
}