using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public GameObject settingPanel;
    public void PlayGame()
    {
        SceneManager.LoadScene("game");
    }

    public void Setting()
    {
        settingPanel.SetActive(true); 
    }

    public void CloseSetting()
    {
        settingPanel.SetActive(false); 
    }

    public void Menu()
    {
        SceneManager.LoadScene("menu");
    }
}
