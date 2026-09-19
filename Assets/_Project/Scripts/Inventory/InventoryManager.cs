using System;
using UnityEngine;
using Farm.Enums;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private ItemSO[] tools;
    [SerializeField] private ItemSO[] items;

    public static InventoryManager Instance {get; private set; }
    private ItemSO equipedItem;
    private ItemSO equipedTool;
    public ItemSO EquipedItem => equipedItem;
    public ItemSO EquipedTool => equipedTool;
    private void OnEnable()
    {
        InventoryEvents.OnSlotClicked += SetEquipedItemOrTool;
        InventoryEvents.OnHandClicked += ResetEquipedItemOrTool;
        InventoryEvents.OnItemAdded += SetEquipedItemOrTool;
    }

    public void ResetEquipedItemOrTool(InventorySlotType type)
    {
        if (type == InventorySlotType.Item)
        {
            for (int i =0; i< items.Length; i++)
            {
                if (items[i] == null)
                {
                    items[i] = equipedItem;
                    equipedItem = null;
                    break;
                }
            }
        }
        else if (type == InventorySlotType.Tool)
        {
            for(int i =0; i< tools.Length; i++)
            {
                if (tools[i] == null)
                {
                    tools[i] = equipedTool;
                    equipedTool = null;
                    break;
                }
            }
        }
    }

    private void OnDisable()
    {
        InventoryEvents.OnSlotClicked -= SetEquipedItemOrTool;
    }
    public void SetEquipedItemOrTool(int index, ItemSO item, InventorySlotType type)
    {

        if (type == InventorySlotType.Item)
        {
            ItemSO prevEquipedItem = equipedItem;
            equipedItem = item;

            if (index <0)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] == null)
                    {
                        index = i;
                        break;
                    }
                }
            }
          
           items[index] = prevEquipedItem;
        }
        else if (type == InventorySlotType.Tool)
        {
            ItemSO prevEquipedTool = equipedTool;
            equipedTool = item;
            tools[index] = prevEquipedTool;
        }
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    
    }
    private void Start()
    {
        if (items.Length == 0)
            items = new ItemSO[12];
        if (tools.Length == 0)
            tools = new ItemSO[12];
        InventoryEvents.OnInventoryStart?.Invoke(tools, items);
        equipedItem = null;
        equipedTool = null;
    }
}
