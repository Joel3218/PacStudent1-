using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string level1Scene;
    public string level0Scene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Level1()
    {
        SceneManager.LoadScene(level1Scene);
    }

    public void Level2()
    {

    }

    public void ExitGame()
    {
        SceneManager.LoadScene(level0Scene);
    }
}

