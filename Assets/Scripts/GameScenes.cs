using UnityEngine.SceneManagement;
using UnityEngine;

public class GameScenes : MonoBehaviour
{
    public bool gameHasComplete = false;
    public bool gameHasOver = false;
    readonly float restartDelay = 1f;

    public GameObject gamecompleteScene;
    public GameObject gameoverScene;

    public void CompleteGame()
    {
        if (gameHasComplete == true && gameHasOver == false)
        {
            gamecompleteScene.SetActive(true);
        }
    }
    public void GameOver()
    {
        gameoverScene.SetActive(true);
    }
    public void FailLevel()
    {
        if (gameHasOver == true && gameHasComplete == false)
        {
            Invoke("GameOver", restartDelay);
            Debug.Log("Game Over");
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
