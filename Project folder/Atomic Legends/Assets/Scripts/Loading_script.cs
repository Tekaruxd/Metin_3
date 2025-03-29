using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading_script : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("Load", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Load()
    {
        SceneManager.LoadScene(2);
    }
}
