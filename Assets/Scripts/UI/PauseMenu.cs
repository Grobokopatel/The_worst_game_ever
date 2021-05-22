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

    public static bool GameIsPaused
    {
        get;
        set;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (GameIsPaused)
                Resume();
            else
                Pause();
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
        Player.player.enabled = true;
    }

    public void Pause()
    {
        if (!allUI.GetComponentsInChildren<Canvas>().Any(canvas => canvas.gameObject.activeInHierarchy))
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0;
            GameIsPaused = true;
            Player.player.enabled = false;
        }
    }

    public void ChangeDifficulty(Text text)
    {
        CirclesOnWater.isCasualDifficultyOn = !CirclesOnWater.isCasualDifficultyOn;
        text.text = $"Сложность: {(CirclesOnWater.isCasualDifficultyOn ? "Казуальная" : "Нормальная")}";
    }
}
