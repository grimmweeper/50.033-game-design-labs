using System.Collections.Generic;
using UnityEngine;

public class SceneVariables : MonoBehaviour
{
    public static SceneVariables Instance;
    
    public List<int> scoreList = new List<int>();
    
    void Awake() {
        if (Instance == null) {
            // scoreSheet += "HIGHSCORE\n";
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } 
        else if (Instance != this){
            Destroy(gameObject);
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
}
