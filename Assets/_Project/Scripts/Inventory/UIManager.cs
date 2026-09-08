using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class UIManager : MonoBehaviour,ITimeTracker
{
    [Header("Inventory")]
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private TMP_Text infoName;
    [SerializeField] private TMP_Text infoDescription;

    [Header("Tools")]
    [SerializeField] private HandInventorySlot handToolSlot;
    [SerializeField] private InventorySlot[] toolsSlots;

    [Header("Items")]
    [SerializeField] private HandInventorySlot handItemSlot;
    [SerializeField] private InventorySlot[] itemSlots;

    [Header("Date and Time")]
    [SerializeField] private TMP_Text date;
    [SerializeField] private TMP_Text time;
    public static UIManager Instance { get; private set; }
    public TMP_Text InfoName => infoName;
    public TMP_Text InfoDescription => infoDescription;
    private void OnEnable()
    {
        InventoryEvents.OnInventoryStart += UpdateInventoryUI;
        InventoryEvents.OnSlotClicked += SetHandSlot;
        InventoryEvents.OnHandClicked += ResetHandSlot;
        InventoryEvents.OnItemAdded += SetHandSlot;

    }
    private void OnDisable()
    {
        InventoryEvents.OnInventoryStart -= UpdateInventoryUI;
        InventoryEvents.OnSlotClicked -= SetHandSlot;
        InventoryEvents.OnHandClicked -= ResetHandSlot;
        TimeManager.instance.RemoveTracker(this);
    }
    private void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }
    private void Start()
    {
        SetSlotsIndcesAndTypes();
        handToolSlot.DisplayItem(null);
        handItemSlot.DisplayItem(null);
        TimeManager.instance.RegisterTracker(this);
    }
    private void UpdateInventoryUI(ItemSO[] tools, ItemSO[] items)
    {
       UpdateSlots(itemSlots, items);
       UpdateSlots(toolsSlots, tools);
    }
    private void UpdateSlots(InventorySlot[] slots, ItemSO[] items)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Length)
                slots[i].DisplayItem(items[i]);
            else
                slots[i].DisplayItem(null);
        }
    }
    private void SetHandSlot(int index, ItemSO item, InventorySlotType type)
    {
        if (type == InventorySlotType.Item)
        {
            ItemSO currentHandItem = handItemSlot.CurrentItem;
            if (index < 0)
            {
                for (int i = 0; i < itemSlots.Length; i++)
                {
                    if (itemSlots[i].CurrentItem == null)
                    {
                        index = i;
                        break;
                    }
                }
            }
            handItemSlot.DisplayItem(item);
            itemSlots[index].DisplayItem(currentHandItem);
        }
        else if (type == InventorySlotType.Tool)
        {
            ItemSO currentHandTool = handToolSlot.CurrentItem;
            handToolSlot.DisplayItem(item);
            toolsSlots[index].DisplayItem(currentHandTool);
        }
    }
    private void ResetHandSlot(InventorySlotType type)
    {
        if (type == InventorySlotType.Item)
        {
            foreach (InventorySlot slot in itemSlots)
            {
                if (slot.CurrentItem == null)
                {
                    slot.DisplayItem(handItemSlot.CurrentItem);
                    handItemSlot.DisplayItem(null);
                    break;
                }
            }
        }
        else if (type == InventorySlotType.Tool)
        {
            foreach (InventorySlot slot in toolsSlots)
            {
                if (slot.CurrentItem == null)
                {
                    slot.DisplayItem(handToolSlot.CurrentItem);
                    handToolSlot.DisplayItem(null);
                    break;
                }
            }
        }
    }
    private void SetSlotsIndcesAndTypes()
    {
        SetSlotIndexAndType(itemSlots, InventorySlotType.Item);
        SetSlotIndexAndType(toolsSlots, InventorySlotType.Tool);
    }
    private void SetSlotIndexAndType(InventorySlot[] slots, InventorySlotType type)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetSlotIndex(i);
            slots[i].SetSlotType(type);
        }
    }
    public void ManageInventoryVisability()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);  
    }

    public void ClockUpdated(GameTimeStamp timeStamp)
    {
        int hours = timeStamp.Hour;
        int minutes = timeStamp.Minute;

        string postfix = " AM";
        if(hours > 12)
        {
            hours -= 12;
            postfix = " PM";
        }
        time.text = hours + ":" + minutes.ToString("00") + postfix;

        int day = timeStamp.Day;
        string dayOfTheWeek = timeStamp.GetDayOfTheWeek().ToString();
        date.text = dayOfTheWeek + ". " + day;
    }
}
