using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Button priceButton;
    public Image itemImage;
    public Text itemName;
    public Item item;
    public Item Gold;
    public Item Crystal;
    private GameObject getEffectPrefab;
    public AudioClip cashSFX;

    public Inventory inventory;
    public Collections collections;

    void Start()
    {
        cashSFX = Resources.Load<AudioClip>("SFX/buy_cashregister_01");
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        collections = GameObject.Find("Collection").GetComponent<Collections>();
        getEffectPrefab = Resources.Load<GameObject>("GetEffectUI");
    }

    public void SetItem(Item _item)
    {
        item = _item;
        itemName.text = item.ItemName;
        itemImage.sprite = item.itemImage;
        itemImage.type = 0;
        itemImage.preserveAspect = true;
        priceButton.GetComponentInChildren<Text>().text = item.ItemPrice.ToString();

        if (item.itemPriceType == Item.ItemPriceType.CRYSTAL)
            priceButton.transform.GetChild(0).GetComponent<Image>().sprite = Crystal.itemImage;
        else
            priceButton.transform.GetChild(0).GetComponent<Image>().sprite = Gold.itemImage;
        CheckPrice();
    }
    public void ClearSlot()
    {
        Destroy(this.gameObject);
    }

    private void CheckPrice()
    {
        if (GameManager.gameManager.IsBuyable(item.ItemPrice, (int)item.itemPriceType))
        {
            priceButton.interactable = true;
        }
        else
        {
            priceButton.interactable = false;
        }
    }

    public void BuyItem()
    {
        if (GameManager.gameManager.IsBuyable(item.ItemPrice, (int)item.itemPriceType))
        {
            inventory.AcquireItem(item);
            collections.Collect(item);
            if (item.itemPriceType == Item.ItemPriceType.CRYSTAL)
            {
                GameManager.gameManager.DecreaseCrystal(item.ItemPrice);
            }
            else
            {
                GameManager.gameManager.DecreaseGold(item.ItemPrice);
            }
            CheckPrice();
            ShowGetEffect(priceButton.transform, item);
        }
        SoundManager.soundManager.PlaySfx(this.transform.position, cashSFX);
    }
    private void ShowGetEffect(Transform tr, Item item, int quantity = 1)
    {
        GameObject obj = Instantiate(getEffectPrefab, tr.position, tr.rotation);
        obj.GetComponent<CsScore>().ChangeInfo(item, quantity);
    }


}
