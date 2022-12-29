using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowItemInfo : MonoBehaviour
{
    public GameObject ClosePanel;
    public Item item = null;
    [SerializeField]
    private Text text_count;
    [SerializeField]
    private GameObject go_CountImage;
    private Image itemImage;
    private Text itemName;
    private Text itemDesc;
    public static ShowItemInfo showItemInfo;
    private CanvasGroup cg;
    public bool isShowing = false;
    Inventory inventory;
    public Button UseBtn;
    public Button ThrowBtn;
    public GameObject tdPanel;
    private int curCount;
    public int throwQuantity=1;
    public Slider throwSlider;
    private CharacterCtrl characterCtrl;

    private void Awake()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemName = transform.GetChild(1).GetComponent<Text>();
        itemDesc = transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>();
        ClosePanel.SetActive(false);

        showItemInfo = this;
        cg = GetComponent<CanvasGroup>();
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
        inventory = GameObject.Find("Inventory").transform.GetComponent<Inventory>();
        tdPanel.SetActive(false);
        characterCtrl = GameObject.FindWithTag("CHARACTER").transform.GetComponent<CharacterCtrl>();
    }
    public void ShowInfo(Item _item, int _itemCount)
    {
       // if (_itemCount == 0) CloseItemInfo();
        if (!isShowing)
        {
            ClosePanel.SetActive(true);
            isShowing = true;
            cg.alpha = 1;
            cg.interactable = true;
            cg.blocksRaycasts = true;
            item = _item;
            itemImage.sprite = _item.itemImage;
            itemImage.type = 0;
            itemImage.preserveAspect = true;
            itemName.text = _item.ItemName;
            itemDesc.text = _item.ItemDesc;
            curCount = _itemCount;
            if (item.itemType != Item.ItemType.COLLECT)
            {
                go_CountImage.SetActive(true);
                text_count.text = curCount.ToString();
                UseBtn.interactable = true;
                ThrowBtn.interactable = true;
            }
            else
            {
                text_count.text = "0";
                go_CountImage.SetActive(false);
                UseBtn.interactable = false;
                ThrowBtn.interactable = false;
            }

        }
        else
        {
            CloseItemInfo();
        }
    }

    public void CloseItemInfo()
    {
        ClosePanel.SetActive(false);
        isShowing = false;
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void UseItem()
    {
        inventory.AcquireItem(item, -1);
        curCount--;
        text_count.text = curCount.ToString();

        if(item.itemType==Item.ItemType.GIFT)
        {
            if (characterCtrl.isHome) return;
            GIFT gift = (GIFT)item;
            if (gift.itemEffectType == GIFT.ItemEffectType.ENERGY)
            {
                characterCtrl.UpdateEnergyBar(gift.ItemEffectValue);
                int rand = Random.Range(0,3);
                if (rand == 0)
                    characterCtrl.Talk("어쩐지 기운이 조금 나는 거 같아.");
                else if (rand == 1)
                    characterCtrl.Talk("힘이 솟는다!");
                else if (rand==2)
                    characterCtrl.Talk("조금 더 놀다갈 수 있겠다!");
                else
                    characterCtrl.Talk("오오! 고마워~!!");
            }
            else if(gift.itemEffectType == GIFT.ItemEffectType.LIKE)
            {
                characterCtrl.UpdateLikeBar(gift.ItemEffectValue);
                int rand = Random.Range(0, 3);
                if (rand == 0)
                    characterCtrl.Talk("내가 이거 좋아하는 거 어떻게 알았어?");
                else if (rand == 1)
                    characterCtrl.Talk("기뻐~!!!");
                else if (rand == 2)
                    characterCtrl.Talk("이거 좋아~!! 고마워!");
                else
                    characterCtrl.Talk("헤헤, 소중히 할게!");
            }
            characterCtrl.PlayAnimation("안녕");
        }
        else if(item.itemType==Item.ItemType.USED)
        {

        }

        if (curCount<=0)
            CloseItemInfo();

    }

    public void OpenThrowDetail()
    {
        tdPanel.SetActive(true);

        throwSlider.minValue = 1f;
        throwSlider.maxValue = curCount;
        tdPanel.transform.GetChild(0).GetComponent<Text>().text = throwQuantity.ToString() + "개";
    }
    public void CloseThrowDetail()
    {
        tdPanel.SetActive(false);
    }

    public void SetQuantity(float sliderValue)
    {                
        throwQuantity = (int)sliderValue;
        tdPanel.transform.GetChild(0).GetComponent<Text>().text = throwQuantity.ToString() + "개";
    }


    public void ThrowItem()
    {
        inventory.AcquireItem(item, -throwQuantity);
        curCount-=throwQuantity;
        text_count.text = curCount.ToString();
        tdPanel.SetActive(false);
        if (curCount <= 0)
            CloseItemInfo();
    }

}
