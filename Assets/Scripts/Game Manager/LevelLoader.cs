using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator anim;

    public float loadTime = 1f;

    public int buildIndex;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LoadNextLevel();
        }
    }
    void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + buildIndex));
    }
    IEnumerator LoadLevel(int levelIndex)
    {
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(loadTime);

        SceneManager.LoadScene(levelIndex);
    }
}
