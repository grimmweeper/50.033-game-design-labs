using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class MenuController : MonoBehaviour
{
    
    
    private GameObject[] restartObjects;
    
    void Awake() {
        Time.timeScale = 0.0f;

        restartObjects = GameObject.FindGameObjectsWithTag("Restart");
        foreach (GameObject ro in restartObjects) {
            Debug.Log("Restart Objects to set inactive found. Name: " + ro.name);
            ro.SetActive(false);
        }
    
    }
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField] private void StartButtonClicked()
    {
        foreach (Transform eachChild in transform)
        {
            if (eachChild.name != "Score")
            {
                Debug.Log("Child found. Name: " + eachChild.name);
                // disable them
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }

    private string generateHighscoreText(List<int> scoreList) {
        
        string highscoreText = "HIGHSCORE\n";

        for (int i=0; i<scoreList.Count; i++) {
            highscoreText += String.Format("{0}. {1}\n", i+1, scoreList[i]);
        }

        return highscoreText;
    }

    public void GameOver(int score) {
        foreach (GameObject ro in restartObjects) {
            Debug.Log("Restart Objects to set active found. Name: " + ro.name);
            ro.SetActive(true);
            if (ro.name == "HighscoreText") {

                SceneVariables.Instance.scoreList.Add(score);
                SceneVariables.Instance.scoreList.Sort();
                SceneVariables.Instance.scoreList.Reverse();

                string scoreSheet = generateHighscoreText(SceneVariables.Instance.scoreList);
                ro.GetComponent<Text>().text = scoreSheet;
            }
        }


        Time.timeScale = 0.0f;
    }

    public void RestartButtonClicked() {
        Debug.Log("Restarting Game...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
