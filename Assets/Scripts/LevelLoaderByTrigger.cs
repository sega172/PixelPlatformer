using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderByTrigger : MonoBehaviour
{
    [SerializeField] private string SceneName = "MainMenu";
    [SerializeField] AudioClip PassedClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ProcessTrigger(collision.gameObject).Forget();
        }
    }

    private async UniTaskVoid ProcessTrigger(GameObject other)
    {
        //притягиваем игрока к воротам
        other.transform.position = transform.position;
        SoundManager.PlaySfx(PassedClip);

        if(other.TryGetComponent(out SpriteRenderer renderer))
        {
            while(renderer.color.a != 0)
            {
                Color color = renderer.color;
                color.a = Mathf.MoveTowards(color.a, 0, Time.deltaTime * 2);
                renderer.color = color;
                await UniTask.WaitForEndOfFrame();
            }
        }
        SceneManager.LoadScene(SceneName);




    }
}
