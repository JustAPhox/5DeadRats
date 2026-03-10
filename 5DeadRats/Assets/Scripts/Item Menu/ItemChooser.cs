using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemChooser : MonoBehaviour
{
    [SerializeField]
    TextAsset itemJSON;

    List<ItemInfo> mildItems;
    List<ItemInfo> matureItems;
    List<ItemInfo> stinkyItems;
    List<ItemInfo> blueItems;
    List<ItemInfo> moldyItems;



    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void Awake()
    {


        //JsonUtility.FromJson<List<ItemInfo>>(jsonFile.text);

        ItemListObject itemList = JsonUtility.FromJson<ItemListObject>(itemJSON.text);

        mildItems = itemList.mildItems;
        matureItems = itemList.matureItems;
        stinkyItems = itemList.stinkyItems;
        blueItems = itemList.blueItems;
        moldyItems = itemList.negativeItems;




    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int[] GiveItem(int wantedItemList)
    {
        bool keepItem = false;

        int[] itemCode = new int[2];
        itemCode[0] = wantedItemList;

        while (!keepItem)
        {
            if (wantedItemList == 0)
            {
                itemCode[1] = UnityEngine.Random.Range(0, mildItems.Count);
            }
            else if (wantedItemList == 1)
            {
                itemCode[1] = UnityEngine.Random.Range(0, matureItems.Count);
            }
            else if (wantedItemList == 2)
            {
                itemCode[1] = UnityEngine.Random.Range(0, stinkyItems.Count);
            }
            else if (wantedItemList == 3)
            {
                itemCode[1] = UnityEngine.Random.Range(0, blueItems.Count);
            }
            else if (wantedItemList == 4)
            {
                itemCode[1] = UnityEngine.Random.Range(0, moldyItems.Count);
            }
            else
            {
                Debug.Log("Wanted ItemList in Give Item Is wrong");
            }

            if (getItemInfo(itemCode).keepChance >= UnityEngine.Random.value)
            {
                keepItem = true;
            }
            
        }

        return itemCode;
    }


    public int[] GivePositiveItem()
    {
        float randomItemValue = UnityEngine.Random.value;

        if (randomItemValue <= 0.5)
        {
            return GiveItem(0);
        }
        else if (randomItemValue <= 0.8)
        {
            return GiveItem(1);
        }
        else if (randomItemValue <= 0.95)
        {
            return GiveItem(2);
        }
        else
        {
            return GiveItem(3);
        }



    }


    public int[] GiveNegativeItem()
    {
        return GiveItem(4);
    }

    public ItemInfo getItemInfo(int[] itemCode)
    {
        if (itemCode[0] == 0)
        {
            return mildItems[itemCode[1]];
        }
        else if (itemCode[0] == 1)
        {
            return matureItems[itemCode[1]];
        }
        else if (itemCode[0] == 2)
        {
            return stinkyItems[itemCode[1]];
        }
        else if (itemCode[0] == 3)
        {
            return blueItems[itemCode[1]];
        }
        else if (itemCode[0] == 4)
        {
            return moldyItems[itemCode[1]];
        }
        else
        {
            Debug.Log("Get Item Info Given wrong item code");
            return mildItems[0];
        }
    }
}

[System.Serializable]
public class ItemListObject
{
    public ItemListObject()
    {
        mildItems = new List<ItemInfo>();
        matureItems = new List<ItemInfo>();
        stinkyItems = new List<ItemInfo>();
        blueItems = new List<ItemInfo>();
        negativeItems = new List<ItemInfo>();

    }
    public List<ItemInfo> mildItems;
    public List<ItemInfo> matureItems;
    public List<ItemInfo> stinkyItems;
    public List<ItemInfo> blueItems;
    public List<ItemInfo> negativeItems;
}


[System.Serializable]
public class ItemInfo
{
    public ItemInfo()
    {
        name = "Name";
        code = "code";
        description = "Stuff About the Item";

        health = 0;
        damage = 0;
        speed = 0;
        vision = 0;
        crit = 0;
        keepChance = 1;
    }


    public string name;
    public string code;
    public string description;

    public int health;
    public int damage;
    public int speed;
    public int vision;
    public int crit;
    public float keepChance;
}