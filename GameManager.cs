using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Singleton instance
    public static GameManager Instance { get; private set; }

    // Game state variables
    public bool isGamePaused = false;
    private float gameTime = 0f;

    // Other game-specific variables like player data, game settings, etc.

    private int currentSceneIndex = 0;

    void Awake()
    {
        // Implement Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        // Handle game-wide updates, if necessary
        UpdateGameTime();

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log(GetGameTime());
        }


    }

    void InitializeGame()
    {
        // Initialize game components here
        // Example: Load player data, set up the world, etc.
    }

    private void UpdateGameTime()
    {
        gameTime += Time.deltaTime;
    }

    public float GetGameTime()
    {
        return gameTime;
    }

    public void PauseGame()
    {
        isGamePaused = true;
        // Implement pause logic, like stopping time and showing pause menu
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        // Implement resume logic
    }

    // Other methods for game progression, saving/loading, etc.

    public void ChangeScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

}
