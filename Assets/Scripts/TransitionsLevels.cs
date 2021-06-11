using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionsLevels : MonoBehaviour
{
    public string levelName;
    public Animator SceneTransition;
    public int timeTransition;

    // Start is called before the first frame update
    void Start()
    {
        PauseMenu.inputEnable = false;
        StartCoroutine(FadeLevel());
    }

    public void LoadLevels()
    {
        Debug.Log("press");
        //SceneManager.LoadScene("Level1");
    }

    IEnumerator FadeLevel()
    {
        yield return new WaitForSeconds(timeTransition);
        SceneTransition.SetTrigger("Start");
        PauseMenu.inputEnable = true;
        SceneManager.LoadScene(levelName);
    }
}
