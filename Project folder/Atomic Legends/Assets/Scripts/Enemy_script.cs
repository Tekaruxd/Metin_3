using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class Enemy_script : MonoBehaviour
{
    public Enemy enemy = new Enemy();
    public Animator Enemy_Animator;
    public GameObject Enemy_prefab;
    public Slider slider;
    public Player_script player_script;
    private bool dead = false;
    public Inventory_script inventory_script;
    public Sound_script sound_Script;
    
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy.hp = enemy.max_hp;
        Health(enemy.hp,enemy.max_hp);
        InvokeRepeating("Move_enemy", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hit"))
        {
            enemy.hp -= player_script.player_dmg;
            sound_Script.Enemy_hit();
            
            if (enemy.hp <= 0 && dead == false)
            {
                dead = true;
                Health(0, enemy.max_hp);
                Enemy_Animator.SetTrigger("Being_Hit");
                Invoke("Spawn", 4);
                Invoke("Kill", 10);
                player_script.Enemy_killed(enemy.drops.exp_amount);
                for(int i = 0; i < enemy.drops.items.Count; i++)
                {
                    inventory_script.Add_item(enemy.drops.items[i]);
                }
            }
            Health(enemy.hp,enemy.max_hp);
        }
        if (other.gameObject.CompareTag("Player")){
            Debug.Log("enemy hit");
            player_script.Get_damage(enemy.dmg);
        }

    }
    private void Spawn()
    {
        Instantiate(Enemy_prefab, new Vector3(Random.Range(0, 10), 1.5f, Random.Range(0, 10)), Quaternion.Euler(0, Random.Range(0,20), 0));
    }
    
    private void Kill()
    {
        Destroy(gameObject);
        Debug.Log("Kill!!");
    }
    private void Health(float hp, float maxhp)
    {
        slider.value = hp / maxhp;
        Debug.Log(hp);        
    }
    public void Move_enemy(){
        transform.Rotate(0, -1, 0);
        transform.Translate(0, 0, 0.1f);
    }
    [System.Serializable]
    public class Enemy
    {
        public string name;
        public float max_hp;
        public float hp;
        public float dmg;

        public Enemy_drops drops = new Enemy_drops();

    }
        [System.Serializable]
    public class Enemy_drops
    {
        public int exp_amount;
        public List<Inventory_script.Item> items = new List<Inventory_script.Item>();//import items
    }
    
}   



