using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayModeUIController : MonoBehaviour
{
    public GameObject container;

    public void Start()
    {
        if (container)
        {
            container.SetActive(false);
            Destroy(container);
            container = null;
        }
        container = ServiceLocator.Resolve<IGameService>().LoadState();
    }


    public void GoBack() {
        SceneManager.LoadScene("UITest", LoadSceneMode.Additive);
    }
}
