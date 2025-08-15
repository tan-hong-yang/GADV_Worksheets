using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBackHandler : MonoBehaviour
{
    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }

}
