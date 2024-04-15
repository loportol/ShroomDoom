using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseEnterButton;
    public MushroomInput playerControls;

    private bool isPaused = false;
    private InputAction pause;

    private void Awake()
    {
        playerControls = new MushroomInput();
    }

    // Start is called before the first frame update
    void Start()
    {

        pauseMenu.SetActive(false);
    }

    private void OnEnable()
    {
        pause = playerControls.Player.Pause;
        pause.Enable();
        pause.performed += Pause;

    }

    private void OnDisable()
    {
        pause.Disable();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Pause(InputAction.CallbackContext context)
    {
        Debug.Log("Paused");

        if (context.performed)
        {

            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(pauseEnterButton);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }


    public void ResetGame()
    {
        //reset the level manager
        EventBus.Publish<PlateEvent>(new PlateEvent(0, true));

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        Destroy(PlayMusic.instance.gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

