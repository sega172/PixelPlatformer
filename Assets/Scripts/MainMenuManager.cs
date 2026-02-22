using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject levelsPanel;

    private void Start()
    {
        _gotoStart();
    }
    public void _gotoLevels()
    {
        startPanel.SetActive(false);
        levelsPanel.SetActive(true);
    }
    public void _gotoStart()
    {
        levelsPanel.SetActive(false);
        startPanel.SetActive(true);
    }
}
