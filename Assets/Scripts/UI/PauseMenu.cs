using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private GameObject allUI;
    [SerializeField]
    private GameObject startTip;
    public static bool GameIsPaused
    {
        get;
        set;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (pauseMenuUI.gameObject.activeInHierarchy)
                Resume();
            else
                Pause();
        
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        Player.player.enabled = true;
    }

    public void Pause()
    {
        if (!allUI.GetComponentsInChildren<Canvas>().Any(canvas => canvas.gameObject.activeInHierarchy))
        {
            startTip.SetActive(false);
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0;
            Player.player.enabled = false;
        }
    }

    public void ChangeDifficulty(Text text)
    {
        CirclesOnWater.isCasualDifficultyOn = !CirclesOnWater.isCasualDifficultyOn;
        text.text = $"Сложность: {(CirclesOnWater.isCasualDifficultyOn ? "Казуальная" : "Нормальная")}";
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
