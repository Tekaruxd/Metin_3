using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class Hud_script : MonoBehaviour
{
    private VisualElement root;
    private Label player_name_label;
    private ProgressBar health_bar;
    private Label health_label;
    private ProgressBar mana_bar;
    private Label mana_label;
    private ProgressBar experience_bar;
    private Label experience_label;
    private VisualElement skill_slots;
    public Texture2D skill_icon;
    [SerializeField] Player_script player_script;

    void OnEnable()
    {
        // Získání kořenového elementu z UIDocument
        var ui_document = GetComponent<UIDocument>();
        root = ui_document.rootVisualElement;

        // Navázání UI elementů
        player_name_label = root.Q<Label>("PlayerName");
        health_bar = root.Q<ProgressBar>("HealthBar");
        health_label = root.Q<Label>("HealthLabel");
        mana_bar = root.Q<ProgressBar>("ManaBar");
        mana_label = root.Q<Label>("ManaLabel");
        experience_bar = root.Q<ProgressBar>("ExperienceBar");
        experience_label = root.Q<Label>("ExperienceLabel");
        skill_slots = root.Q<VisualElement>("SkillSlots");


        // Inicializace UI
        Initialize_HUD();


        InvokeRepeating("Update_HUD", 1, 1);

    }

    void Initialize_HUD()
    {
        // Nastavení výchozích hodnot
        player_name_label.text = "Hráč";
        
        // Konfigurace progress barů
        health_bar.lowValue = 0;
        health_bar.highValue = 100;
        health_bar.value = 100;

        mana_bar.lowValue = 0;
        mana_bar.highValue = 100;
        mana_bar.value = 50;

        experience_bar.lowValue = 0;
        experience_bar.highValue = 100;
        experience_bar.value = 0;

        // Vytvoření 6 skill slotů
        for (int i = 0; i < 6; i++)
        {
            var skill_slot = new VisualElement();
            skill_slot.AddToClassList("skill-slot");
            skill_slot.name = $"SkillSlot_{i}";
            skill_slots.Add(skill_slot);
        }
    }

    // Metody pro aktualizaci HUD
    public void Update_player_name(string name)
    {
        player_name_label.text = name;
    }

    public void Update_health(float current_health, float max_health, float gain_health)
    {
        if(player_script.hp < player_script.max_hp)
        {
            player_script.hp += gain_health;
            
        }
        health_label.text = $"{player_script.hp}/{player_script.max_hp}";
        health_bar.lowValue = 0;
        health_bar.highValue = max_health;
        health_bar.value = current_health;
    }

    public void Update_mana(float current_mana, float max_mana, float gain_mana)
    {
        if(player_script.mana < player_script.max_mana)
        {
            player_script.mana += gain_mana;
        }
        mana_label.text = $"{player_script.mana}/{player_script.max_mana}";
        mana_bar.lowValue = 0;
        mana_bar.highValue = max_mana;
        mana_bar.value = current_mana;
    }

    public void Update_experience(float current_exp, float max_exp)
    {
        experience_label.text = $"{player_script.exp}/{player_script.max_exp}";
        experience_bar.lowValue = 0;
        experience_bar.highValue = max_exp;
        experience_bar.value = current_exp;
        
    }

    public void Set_skill_icon(int slot_index, Texture2D icon)
    {
        if (slot_index >= 0 && slot_index < 6)
        {
            var skill_slot = root.Q<VisualElement>($"SkillSlot_{slot_index}");
            skill_slot.style.backgroundImage = icon;
            skill_slot.style.width = 100;
        }
    }

    void Update_HUD()
    {
        Debug.Log("Updating HUD");
        Update_player_name(player_script.player_name);
        Update_health(player_script.hp, player_script.max_hp, player_script.hp_per_sec);
        Update_mana(player_script.mana, player_script.max_mana, player_script.mana_per_sec);
        Update_experience(player_script.exp, player_script.max_exp);

        
    }
    
    //Import Ability
    /*void Ui_ability_on_cooldown(Ability ability)
    {
        // Update Icon so its dark or something
    }*/
}
