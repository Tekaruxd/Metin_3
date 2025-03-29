using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Inventory_script : MonoBehaviour
{
    [SerializeField] Game_script game_script;
    public Inventory inventory = new Inventory();
    public delegate void OnItemDragEnter(VisualElement slot);
    public delegate void OnItemDragLeave(VisualElement slot);
    public delegate void OnItemDragExited(VisualElement slot);
    public event OnItemDragEnter OnDragEnterEvent;
    public event OnItemDragLeave OnDragLeaveEvent;
    public event OnItemDragExited OnDragExitedEvent;
    private UIDocument ui_document;
    private VisualElement root;
    private VisualElement equipment_container;
    private VisualElement inventory_container;
    private VisualElement main_container;
    private VisualElement all_container;
    private Label money_label;
    private int player_money = 0;
    private bool is_inventory_visible = true;
    private const int EQUIPMENT_SLOTS = 8;
    private const int INVENTORY_SLOTS = 32;
    private const int INVENTORY_ROWS = 4;
    private const int INVENTORY_COLUMNS = 8;
    private void OnEnable()
    {
        ui_document = GetComponent<UIDocument>();
        root = ui_document.rootVisualElement;
        Initialize_ui();
        Create_inventory_slots();
        Update_money_display();
        //Load_items_from_json();
        main_container.style.display = DisplayStyle.Flex;
        Toggle_inventory();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Toggle_inventory();
            game_script.Show_cursor();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            Load_items_from_json();
        }
    }
    private void Toggle_inventory()
    {
        is_inventory_visible = !is_inventory_visible;
        all_container.style.display = is_inventory_visible ? DisplayStyle.Flex : DisplayStyle.None;
    }
    private void Initialize_ui()
    {
        all_container = root.Q<VisualElement>("all-container");
        main_container = root.Q<VisualElement>("main-container");
        equipment_container = root.Q<VisualElement>("equipment-container");
        inventory_container = root.Q<VisualElement>("inventory-container");
        money_label = root.Q<Label>("money-label");
    }
    private void Create_inventory_slots()
    {
        for (int i = 0; i < EQUIPMENT_SLOTS; i++)
        {
            VisualElement slot = Create_inventory_slot();
            slot.name = $"equipment-slot-{i}";
            equipment_container.Add(slot);
            slot.style.width = new Length(100f / EQUIPMENT_SLOTS, LengthUnit.Percent);
            slot.style.height = new Length(100f / INVENTORY_ROWS, LengthUnit.Percent);
        }
        for (int row = 0; row < INVENTORY_ROWS; row++)
        {
            for (int col = 0; col < INVENTORY_COLUMNS; col++)
            {
                int index = row * INVENTORY_COLUMNS + col;
                VisualElement slot = Create_inventory_slot();
                slot.name = $"inventory-slot-{index}";
                slot.style.width = new Length(100f / INVENTORY_COLUMNS, LengthUnit.Percent);
                slot.style.height = new Length(100f / INVENTORY_ROWS, LengthUnit.Percent);
                inventory_container.Add(slot);
            }
        }
    }
    private VisualElement Create_inventory_slot()
    {
        VisualElement slot = new VisualElement();
        slot.AddToClassList("inventory-slot");
        VisualElement inner_slot = new VisualElement();
        inner_slot.name = "inner-slot";
        inner_slot.AddToClassList("inner-slot");
        slot.Add(inner_slot);
        inner_slot.RegisterCallback<PointerEnterEvent>(On_pointer_enter);
        inner_slot.RegisterCallback<PointerLeaveEvent>(On_pointer_leave);
        inner_slot.RegisterCallback<PointerUpEvent>(On_pointer_up);
        return slot;
    }
    
    private void Load_items_from_json()
    {
        VisualElement inventory_element = inventory_container;
        game_script.Load_inventory();
        foreach (var item in inventory.items)
        {
            VisualElement empty_slot = Find_empty_slot();
            if (empty_slot == null)
            {
                break;
            }
            Update_inventory(item, empty_slot);
        }
    }
    
    public void Add_item(Item _item)
    {
        bool item_exist = false;
        foreach (var item in inventory.items)
        {
            if (item.name == _item.name)
            {
                Debug.Log("item" + item.count);
                Debug.Log("item added" + _item.count);
                item_exist = true;
                if (item.count + _item.count > item.stack){
                    item_exist = false;
                    
                }
                else{
                    item.count += _item.count;
                }
                
            }

                        
        }
        if(item_exist == false){
            inventory.items.Add(_item);
            VisualElement empty_slot = Find_empty_slot();
            if (empty_slot == null)
            {
                return;
            }
            Update_inventory(_item, empty_slot);
        }
    }
    
    public void Update_inventory(Item item,VisualElement empty_slot)
    {
        VisualElement item_element = new VisualElement();
        item_element.name = item.name;
        item_element.AddToClassList("item");
        item_element.style.backgroundImage = item.icon;
        item_element.style.width = new Length(100f, LengthUnit.Percent);
        item_element.style.height = new Length(100f, LengthUnit.Percent);
        empty_slot.Add(item_element);

    }
    
    public void Add_money(int amount)
    {
        player_money += amount;
        Update_money_display();
    }
    private void Update_money_display()
    {
        money_label.text = $"Peníze: {player_money}";
    }
    private VisualElement Find_empty_slot()
    {
        foreach (VisualElement slot in inventory_container.Children())
        {
            VisualElement inner_slot = slot.Q<VisualElement>("inner-slot");
            if (!inner_slot.Children().GetEnumerator().MoveNext())
                return inner_slot;
        }
        return null;
    }
    public void On_pointer_enter(PointerEnterEvent evt)
    {
        VisualElement slot = evt.currentTarget as VisualElement;
        Debug.Log("Pointer entered a slot");
        OnDragEnterEvent?.Invoke(slot);
    }
    public void On_pointer_leave(PointerLeaveEvent evt)
    {
        VisualElement slot = evt.currentTarget as VisualElement;
        Debug.Log("Pointer left a slot");
        OnDragLeaveEvent?.Invoke(slot);
    }
    public void On_pointer_up(PointerUpEvent evt)
    {
        VisualElement slot = evt.currentTarget as VisualElement;
        Debug.Log("Pointer released over a slot");
        OnDragExitedEvent?.Invoke(slot);
    }
    [System.Serializable]
    public class Inventory
    {
        public int gold;
        public bool is_full;
        public List<Item> items = new List<Item>();
    }
    [System.Serializable]
    public class Item
    {
        public string name;
        public int count;
        public string description;
        public Texture2D icon;
        public int stack;
    }
}


