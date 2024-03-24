using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { speedUp, speedBigUp, getMoney };

[System.Serializable]
public class ItemData
{
    public string Name;
    public Sprite itemImage;
    public ItemType type;
}

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;

    [SerializeField] private List<ItemData> datas;
    [SerializeField] private Image itemImage;
    [SerializeField] private Text itemName;

    public CarMove player;

    public ItemData currItemData;

    private void Awake()
    {
        instance = this;
    }

    public void GetItem()
    {
        int ranNum = Random.Range(0, datas.Count);

        currItemData = datas[ranNum];
        ItemInit();
    }

    public void ResetItem()
    {
        itemImage.sprite = null;
        itemName.text = null;
    }

    private void ItemInit()
    {
        itemImage.sprite = currItemData.itemImage;
        itemName.text = currItemData.Name;

        if(currItemData.type == ItemType.speedUp)
        {
            SpeedUp();
        }
        else if(currItemData.type == ItemType.speedBigUp)
        {
            SpeedBigUp();
        }
        else if(currItemData.type == ItemType.getMoney)
        {
            GetMoney();
        }
    }

    private void SpeedUp()
    {

    }

    private void SpeedBigUp()
    {

    }

    private void GetMoney()
    {

    }
}
