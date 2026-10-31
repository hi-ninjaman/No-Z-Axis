using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseContainer;

    public GameObject RPlayer;

    public GameObject ShopContainer;

    public InputActionReference pauseAction;

    public static Vector2 savedPosition = Vector2.zero;

    public PlayerMovement player;

    public Image healthImg;

    private bool isPaused;
    
    void OnEnable()
    {
        pauseAction.action.performed += OnPause;
        pauseAction.action.Enable();
    }

    void OnDisable()
    {
        pauseAction.action.performed -= OnPause;
    }

    void OnPause(InputAction.CallbackContext context)
    {
        if (isPaused)
            ResumeButton();
        else
            Pause();
    }  

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart(string scenename)
    {
       SceneManager.LoadScene(scenename);
       Time.timeScale = 1f;
    }

    void Pause()
    {
        isPaused = true;
        PauseContainer.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeButton()
    {
        isPaused = false;
        PauseContainer.SetActive(false);
        Time.timeScale = 1f;
    }
    public void MainMenuButton(string scenename)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(scenename);
    }

    public void Shop()
    {
        PauseContainer.SetActive(false);
        ShopContainer.SetActive(true);
    }

    public void HealthPurchase()
    {
        if (player == null)
        {
            Debug.LogError("Player reference is kinda gone gng");
            return;
        }

        if (PlayerMovement.coins >= 3 && player.health < 100)
        {
            Debug.Log("bought!");
            PlayerMovement.coins += -3;
            player.health = 100;
            int health = player.health;
            healthImg.fillAmount = health / 100f;
        }
    }

    public void CheckpointPurchase()
    {
        if (player == null)
        {
            Debug.LogError("Player reference is kinda gone gng");
            return;
        }

        if (PlayerMovement.coins >= 4)
        {
            Debug.Log("bought!");
            PlayerMovement.coins += -4;
            Checkpoint.savedPosition = player.transform.position;
        }
    }

    public void SightPurchase()
    {
        if (player == null)
        {
            Debug.LogError("Player reference is kinda gone gng");
            return;
        }

        if (PlayerMovement.coins >= 2)
        {
            Debug.Log("bought!");
            PlayerMovement.coins += -2;
            freelookScript.offsetAmount += 1f;
        }
    }

    public void Z_AxisPurchase()
    {
        if (player == null)
        {
            Debug.LogError("Player reference is kinda gone gng");
            return;
        }

        if (PlayerMovement.coins >= 2)
        {
            Debug.Log("bought!");
            PlayerMovement.coins += -1;
            RPlayer.transform.rotation = Quaternion.identity;
        }
    }
    
    public void ShopBack()
    {
        PauseContainer.SetActive(true);
        ShopContainer.SetActive(false);
    }

    

}