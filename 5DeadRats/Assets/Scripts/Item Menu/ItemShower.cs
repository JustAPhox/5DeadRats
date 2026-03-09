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
    private GameObject itemImage;

    [SerializeField]
    private Image selectorArrow;

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
        ItemInfo item = itemLogic.GetComponent<ItemChooser>().getItemInfo(itemCode);
        itemNameBox.SetText(item.name);
        itemDescriptionBox.SetText(item.description);
        itemImage.GetComponent<itemIconChooser>().showItem(code);
    }

    public void itemSelected()
    {
        selectorArrow.enabled = true;
    }

    public void itemUnselected()
    {
        selectorArrow.enabled = false;
    }

    public ItemInfo itemBought()
    {
        gameObject.GetComponent<Image>().color = Color.red;
        return itemLogic.GetComponent<ItemChooser>().getItemInfo(itemCode);
    }



}
