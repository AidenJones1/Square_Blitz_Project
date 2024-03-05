using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles everything to do with scene changing.
/// </summary>
public class ManageScene : MonoBehaviour
{
    public static ManageScene Instance { get; private set; }

    public void ToGameScene()
    {
        StartCoroutine(GoToScene(1)); //Loads Game Scene
    }

    public void ToMainMenu()
    {
        StartCoroutine(GoToScene(0)); //Load Main Menu Scene
    }

    private IEnumerator GoToScene(int scene)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(scene);
    }

    private void Start()
    {
        Instance = this;
    }
}
