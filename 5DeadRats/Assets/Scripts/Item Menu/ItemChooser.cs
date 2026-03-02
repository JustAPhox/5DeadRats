using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChooser : MonoBehaviour
{
    string[,] positveItems = {{ "GoodStuff", "Good_Stuff" ,"It Helps" }};
    string[,] negativeItems = {{ "BadStuff", "Bad_Stuff" ,"It Hurts" }};



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public int[] GivePositiveItem()
    {
        int[] itemCode = { 0, Random.Range(0, positveItems.Length - 1) };
        return itemCode;
    }


    public int[] GiveNegativeItem()
    {
        int[] itemCode = { 1, Random.Range(0, negativeItems.Length - 1) };
        return itemCode;
    }

    public int[] GiveRandomItem()
    {
        if (Random.value > 0.25)
        {
            return GivePositiveItem();
        }
        else
        {
            return GiveNegativeItem();
        }
    }

    public string getItemName(int[] itemCode)
    {
        Debug.Log($"Code Part 1: {itemCode[0]}");
        Debug.Log($"Code Part 2: {itemCode[1]}");
        if (itemCode[0] == 0)
        {
            return positveItems[itemCode[1], 0];
        }
        else
        {
            return negativeItems[itemCode[1], 0];
        }
    }

    public string getItemCode(int[] itemCode)
    {
        if (itemCode[0] == 0)
        {
            return positveItems[itemCode[1], 1];
        }
        else
        {
            return negativeItems[itemCode[1], 1];
        }
    }

    public string getItemDescription(int[] itemCode)
    {
        if (itemCode[0] == 0)
        {
            return positveItems[itemCode[1], 2];
        }
        else
        {
            return negativeItems[itemCode[1], 2];
        }
    }


}
