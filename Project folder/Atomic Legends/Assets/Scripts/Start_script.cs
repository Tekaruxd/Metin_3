using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class Start_script : MonoBehaviour
{
    private bool tutorial_visible = false;
    private UIDocument start_document = null;
    private VisualElement root = null;
    private VisualElement tutorial_container = null;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        start_document = GetComponent<UIDocument>();
        root = start_document.rootVisualElement;
        tutorial_container = root.Q<VisualElement>("tutorial-container");
        Toggle_tutorial();
        Toggle_tutorial();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            Toggle_tutorial();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
        private void Toggle_tutorial()
    {
        tutorial_visible = !tutorial_visible;
        tutorial_container.style.display = tutorial_visible ? DisplayStyle.Flex : DisplayStyle.None;
    }
}
