using UnityEngine;
using System.Collections.Generic;

// Load and save data, Game start demendecies,
// Path for saved data: C:\Users\Dpoko\AppData\LocalLow\DefaultCompany\Atomic Legends
// Working on: Universal save and load, save and load for all player data, Save and load inventory 
public class Game_script : MonoBehaviour
{

    public bool show_cursor = true;
    //public Inventory_script.Inventory inventory;
    public int spawn_count = 10;
    public GameObject Enemy_prefab;
    public GameObject player;
    [SerializeField] public Inventory_script inventory_script;
    public Inventory_script.Inventory inventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //inventory = inventory_script.inventory;
        for (int i = 0; i < spawn_count; i++)
        {
            Spawn();
        }
        Load_inventory();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && Input.GetKey(KeyCode.LeftShift))
        {
            Save_player_data();
        }
        if (Input.GetKeyDown(KeyCode.L) && Input.GetKey(KeyCode.LeftShift))
        {
            Load_player_data();
        }
    
    }
    public void Save(string file_name_and_path, string data_to_save)
    {
        string file_path = Application.persistentDataPath + "/" + file_name_and_path + ".json";
        Debug.Log(file_path);
        System.IO.File.WriteAllText(file_path, data_to_save);
    }
    public void Save_inventory()
    {
        inventory = inventory_script.inventory;
        string inventory_data = JsonUtility.ToJson(inventory);
        Save("inventory_data", inventory_data);
        Debug.Log("Saved");
    }
    public string Load(string file_name_and_path)
    {
        string file_path = Application.persistentDataPath + "/" + file_name_and_path + ".json";
        string load_data = System.IO.File.ReadAllText(file_path);
        Debug.Log(file_path);
        return load_data;
    }
    
    public void Load_inventory()
    {
        string inventory_data = Load("inventory_data");
        Inventory_script.Inventory inventory = JsonUtility.FromJson<Inventory_script.Inventory>(inventory_data);
        Debug.Log("Loaded");
    }
    public void Save_player_data()
    {
        Player_data player_data = new Player_data();
        player_data.position = player.transform.position;
        string player_data_json = JsonUtility.ToJson(player_data);
        Save("player_data", player_data_json);
        Debug.Log("Player saved");
    }
    public void Show_cursor()
    {
        show_cursor = !show_cursor;
        if(show_cursor == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
        public void Load_player_data()
    {
        Debug.Log("Player loading");
        string player_data_json = Load("player_data");
        Player_data player_data = JsonUtility.FromJson<Player_data>(player_data_json);
        player.transform.position = player_data.position;
        Debug.Log("Loaded");
    }
     public void Spawn()
    {
         Instantiate(Enemy_prefab, new Vector3(Random.Range(0, 10), 1.5f, Random.Range(0, 10)), Quaternion.Euler(0, Random.Range(0,360), 0));
    }

[System.Serializable]
public class Player_data
{
    public Vector3 position;
}
}




