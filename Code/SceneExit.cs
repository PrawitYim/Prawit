using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExit : MonoBehaviour
{
    public string sceneToload;
    public string exitName;
    private void OnTriggerEnter(Collider other)
    {
        PlayerPrefs.SetString("LastExitName", exitName);
        SceneManager.LoadScene(sceneToload);
    }
}
