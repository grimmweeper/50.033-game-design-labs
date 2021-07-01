using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManagerEV : MonoBehaviour
{
    public UnityEvent onApplicationExit;
	void OnApplicationQuit()
    {
        onApplicationExit.Invoke();
    }
}