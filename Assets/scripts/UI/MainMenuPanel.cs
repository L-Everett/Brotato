using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    private Button startButton;
    private Button settingsButton;
    private Button progressButton;
    private Button exitButton;

    private void Awake()
    {
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        settingsButton = GameObject.Find("SettingsButton").GetComponent<Button>();
        progressButton = GameObject.Find("ProgressButton").GetComponent<Button>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
    }

    private void Start()
    {
        startButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("02-LevelSelect");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
