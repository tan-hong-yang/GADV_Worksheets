using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    public void LevelOne()
    {
        SceneManager.LoadScene(2);
    }
    public void LevelTwo()
    {
        SceneManager.LoadScene(3);
    }
}
