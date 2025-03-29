using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Abilities_script : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Player_script player_script;
    [SerializeField] Hud_script hud_script;
    [Header("Abilities")]
    [SerializeField] public List<Ability> abilities = new List<Ability>();


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i = 0; i < abilities.Count; i++)
        {
            hud_script.Set_skill_icon(i, abilities[i].icon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i <= 3; i++)
        {
            int keyNumber = (i + 1) % 10;
            if (Input.GetKeyDown(KeyCode.Alpha0 + keyNumber))
            {
                Activate_ability(abilities[i]);
                break;
            }
        }
    }

    void Activate_ability(Ability ability)
    {
        if (ability.ready == false)
        {
            return;
        }
        ability.ready = false;
        switch (ability.slot)
        {
            case 0:
                Use_ability_1();
                break;
            case 1:
                Use_ability_2();
                break;
            case 2:
                Use_ability_3();
                break;
            case 3:
                Use_ability_4();
                break;
        }
        StartCoroutine(Ability_cooldown_coroutine(ability));
    }

    IEnumerator Ability_cooldown_coroutine(Ability ability)
    {
        yield return new WaitForSeconds(ability.cooldown);
        Ability_on_cooldown(ability);
    }

    void Ability_on_cooldown(Ability ability){
        Debug.Log(ability.name + "Ability is ready!");
        ability.ready = true;
    }

    void Use_ability_1()
    {
        // Instant ability no need to chect duration
        Ability _ability = abilities[0];
        if(_ability.active == true)
        {
            // Opak co to aktivuje
            _ability.active = false;
            return;
        }
        if(player_script.mana < _ability.mana_cost)
        {
            Debug.Log("Not enough mana!");
            return;
        }
        
        Debug.Log(_ability.name + " used!");
        player_script.mana -= _ability.mana_cost;
        Vector3 dashDirection = player_script.transform.forward * 10;
        player_script.controller.Move(dashDirection);
    }

    void Use_ability_2()
    {
        Ability _ability = abilities[1];
        if(_ability.active == true)
        {
            player_script.player_dmg -= 3;
            // Opak co to aktivuje
            _ability.active = false;
            return;
        }
        if(player_script.mana < _ability.mana_cost)
        {
            Debug.Log("Not enough mana!");
            return;
        }
        
        Debug.Log(_ability.name + " used!");
        player_script.mana -= _ability.mana_cost;
        player_script.player_dmg += 3;
        _ability.active = true;
        Invoke("Use_ability_2",_ability.duration);
    }

    void Use_ability_3()
    {
        
    }

    void Use_ability_4()
    {
        
    }	

    [System.Serializable] 
    public class Ability
    {
        public int slot = 0;
        public string name = "";
        public string description = "";
        public bool ready = false;
        public bool active = false;
        public float mana_cost = 0;
        public float cooldown = 0;
        public float duration = 0;
        public Texture2D icon = null;
    }
}