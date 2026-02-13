using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderByTrigger : MonoBehaviour
{
    [SerializeField] private string SceneName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}
