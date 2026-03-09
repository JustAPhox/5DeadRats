using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemIconChooser : MonoBehaviour
{
    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private Sprite noIcon;


    [SerializeField]
    private Sprite[] mildItemSprites;    
    [SerializeField]
    private Sprite[] matureItemSprites;    
    [SerializeField]
    private Sprite[] stinkyItemSprites;    
    [SerializeField]
    private Sprite[] blueItemSprites;
    [SerializeField]
    private Sprite[] negativeItemSprites;

    private Sprite[] wantedSpriteArray;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void updateImage(Sprite itemSprite)
    {
        itemImage.sprite = itemSprite;
    }

    public void itemBought()
    {
        itemImage.sprite = noIcon;
    }


    public void showItem(int[] itemCode)
    {

        if (itemCode[0] == 0)
        {
            wantedSpriteArray = mildItemSprites;
        }
        else if (itemCode[0] == 1)
        {
            wantedSpriteArray = matureItemSprites;
        }
        else if (itemCode[0] == 2)
        {
            wantedSpriteArray = stinkyItemSprites;
        }
        else if (itemCode[0] == 3)
        {
            wantedSpriteArray = blueItemSprites;
        }
        else if (itemCode[0] == 4)
        {
            wantedSpriteArray = negativeItemSprites;
        }
        else
        {
            wantedSpriteArray = mildItemSprites;
            Debug.Log("Item Icon Chooser not given correct array");
        }



        // Pos Items
        if (itemCode[1] < wantedSpriteArray.Length)
        {
            itemImage.sprite = wantedSpriteArray[itemCode[1]];
        }
        else
        {
            Debug.Log("ItemIcon Given Item with no image");
        }

    }

}
