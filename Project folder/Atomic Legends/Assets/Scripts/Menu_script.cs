using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UIElements;

public class Menu_script : MonoBehaviour
{
    public Game_script game_script;
    private bool is_button_clicked = false;
    

    private void Start()
    {
        Show_menu();
        Show_menu();
    }
    private void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Show_menu();
        }
        
        Is_button_clicked();

    }

    private void Is_button_clicked()
    {
        if (is_button_clicked) return;

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Button exit_button = root.Q<Button>("exit");
        Button resume_button = root.Q<Button>("resume");

        exit_button.clicked += () => Application.Quit();
        resume_button.clicked += Show_menu;
        Setup_buttons(root);
        is_button_clicked = true;
    }
    private void Setup_buttons(VisualElement root)
    {
        Button save_button = root.Q<Button>("save");
        Button load_button = root.Q<Button>("load");

        save_button.clicked += () =>
        {
            game_script.Save_inventory();
            is_button_clicked = true;
        };
        load_button.clicked += () =>
        {
            game_script.Load_inventory();
            is_button_clicked = true;
        };
    }

    private void Show_menu()
    {
        GetComponent<UIDocument>().rootVisualElement.style.display = 
            GetComponent<UIDocument>().rootVisualElement.style.display == DisplayStyle.Flex ? 
            DisplayStyle.None : DisplayStyle.Flex;
            game_script.GetComponent<Game_script>().Show_cursor();

    }
    
}

