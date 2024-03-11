using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void ExitGame() => Application.Quit();
    public void StartGame() => SceneManager.LoadScene(1);
}
