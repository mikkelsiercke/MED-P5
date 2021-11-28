using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private int sceneNumber;
    
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
