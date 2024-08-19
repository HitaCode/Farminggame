using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSaveState : MonoBehaviour
{
    public List<LandSaveState> landData;
    public List<CropSaveState> cropData;

    public ItemSlotData[] toolSlots;
    public ItemSlotData[] itemSlots;

    public ItemSlotData equippedItemSlot;
    public ItemSlotData equippedToolSlot;
}
