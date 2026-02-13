using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void _LoadLevel(string LevelName) => SceneManager.LoadScene(LevelName);
    
}
