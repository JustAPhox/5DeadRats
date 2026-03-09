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

    List<ItemInfo> positiveItems;
    List<ItemInfo> negativeItems;



    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void Awake()
    {


        //JsonUtility.FromJson<List<ItemInfo>>(jsonFile.text);

        ItemListObject itemList = JsonUtility.FromJson<ItemListObject>(itemJSON.text);

        positiveItems = itemList.positiveItems;
        negativeItems = itemList.negativeItems;


        Debug.Log(itemList.positiveItems[0].name);
        Debug.Log(itemList.negativeItems[0].name);

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
                itemCode[1] = UnityEngine.Random.Range(0, positiveItems.Count);
            }
            else if (wantedItemList == 1)
            {
                itemCode[1] = UnityEngine.Random.Range(0, negativeItems.Count);
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
        return GiveItem(0);
    }


    public int[] GiveNegativeItem()
    {
        return GiveItem(1);
    }

    public int[] GiveRandomItem()
    {
        if (UnityEngine.Random.value > 0.25)
        {
            return GivePositiveItem();
        }
        else
        {
            return GiveNegativeItem();
        }
    }

    public ItemInfo getItemInfo(int[] itemCode)
    {
        if (itemCode[0] == 0)
        {
            return positiveItems[itemCode[1]];
        }
        else
        {
            return negativeItems[itemCode[1]];
        }
    }
}

[System.Serializable]
public class ItemListObject
{
    public ItemListObject()
    {
        positiveItems = new List<ItemInfo>();
        negativeItems = new List<ItemInfo>();
    }
    public List<ItemInfo> positiveItems;
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