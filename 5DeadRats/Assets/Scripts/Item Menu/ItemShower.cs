using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemShower : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI itemNameBox;
    [SerializeField]
    private TextMeshProUGUI itemDescriptionBox;

    public GameObject itemLogic;

    private int[] itemCode;

    [SerializeField]
    private Sprite[] positiveItemSprites;
    [SerializeField]
    private Sprite[] negativeItemSprites;

    [SerializeField]
    private Image itemImage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initialiseBox(int[] code)
    {
        itemCode = code;
        itemNameBox.SetText(itemLogic.GetComponent<ItemChooser>().getItemName(itemCode));
        itemDescriptionBox.SetText(itemLogic.GetComponent<ItemChooser>().getItemDescription(itemCode));
        if (itemCode[0] == 0)
        {
            itemImage.sprite = positiveItemSprites[itemCode[1]];
        }
        else
        {
            itemImage.sprite = negativeItemSprites[itemCode[1]];

        }
    }

    public void itemSelected()
    {
        gameObject.GetComponent<Image>().color = Color.green;
    }

    public void itemUnselected()
    {
        gameObject.GetComponent<Image>().color = Color.white;
    }

    public string itemBought()
    {
        gameObject.GetComponent<Image>().color = Color.red;
        return itemLogic.GetComponent<ItemChooser>().getItemCode(itemCode);
    }



}
