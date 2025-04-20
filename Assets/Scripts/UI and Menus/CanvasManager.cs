using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;
    public bool gamePaused;
    
    [SerializeField] Image _crosshair;

    [Header("Pause Menu")]
    [SerializeField] GameObject _pauseMenu;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        else
            Destroy(gameObject);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

    }

    public void TogglePause()
    {
        gamePaused = !gamePaused;
        _crosshair.enabled = !gamePaused;
        Cursor.lockState = gamePaused ? CursorLockMode.None : CursorLockMode.Locked;

        Time.timeScale = gamePaused ? 0 : 1;
        _pauseMenu.SetActive(gamePaused);
      
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }

    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync("Ravenholm");
        TogglePause();
    }
}
