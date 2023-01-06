using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActionContoller : MonoBehaviour
{
    private Camera cam;
    Vector3 MousePosition;
    Inventory inventory;
    Collections collections;
    [SerializeField]
    Transform canvasUI;
    private GameObject getEffectPrefab;
    public ItemInfo Exp;
    public ItemInfo Gold;
    public ItemInfo Crystal;
    public ItemInfo Energy;
    private readonly string hashItem = "ITEM";
    private readonly string hashCrop = "CROP";
    private readonly string hashFurniture = "FURNITURE";
    private readonly string hashChara = "CHARACTER";
    private readonly string hashDust = "DUST";
    public GameControl gameControl;
    private CharacterCtrl characterCtrl;

    void Start()
    {
        canvasUI = GameObject.Find("Canvas-UI").transform;
        cam = GetComponent<Camera>();
        getEffectPrefab = Resources.Load<GameObject>("GetEffect");
        inventory = GameObject.Find("Inventory").transform.GetComponent<Inventory>();
        collections = GameObject.Find("Collection").transform.GetComponent<Collections>();
        characterCtrl = GameObject.FindWithTag(hashChara).transform.GetComponent<CharacterCtrl>();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (Input.GetMouseButtonDown(0))
        {
            ClickItem();
        }
    }

    private void ClickItem()
    {
        MousePosition = Input.mousePosition;
        MousePosition = cam.ScreenToWorldPoint(MousePosition);

        RaycastHit2D hit = Physics2D.Raycast(MousePosition, -Vector2.up);
        Debug.DrawRay(MousePosition, transform.forward * 20, Color.red, 0.3f);
        if (hit.collider.tag == hashItem)
        {
            GetItem(hit);
        }
        else if (hit.collider.tag == hashCrop)
        {
            GetCrop(hit);
        }
        else if(hit.collider.tag == hashFurniture)
        {
            GetFurniture(hit);
        }
        else if (hit.collider.tag == hashChara)
        {
            if (gameControl.isEditable) return;
            if (characterCtrl.isReaction) return;
            characterCtrl.StartCoroutine("Reaction");
        }
        else if (hit.collider.tag == hashDust)
        {
            if (!gameControl.isEditable && !characterCtrl.isHome)
            {
                characterCtrl.StopCoroutine("ChooseAction");
                GetDust(hit);
              //  StartCoroutine(RemoveDust(hit));
            }
        }

    }

    private void GetCrop(RaycastHit2D hit)
    {

        Crop crop = hit.collider.GetComponent<Crop>();
        if (crop.isComplete == true)
        {
              if (gameControl.isEditable) return;
            Item item = hit.transform.GetComponent<ItemInfo>().item;
            inventory.AcquireItem(item, crop.quantity);
            collections.Collect(item);
            GameManager.gameManager.IncreaseExp(crop.exp);
            Destroy(hit.transform.gameObject);

            ShowGetEffect(hit.transform, item, crop.quantity, Exp.item, (int)crop.exp);
            Building cropPlace = crop.GetComponent<Building>();
            GridBuildingSystem.gbSystem.ClearArea(cropPlace.area);
        }
        else
        {            
            crop.ShowLeftTime();
            Building cropPlace= crop.GetComponent<Building>();
            if(!cropPlace.Placed)
            {
                if (cropPlace.CanBePlaced())
                {
                    cropPlace.Place();
                }
            }
        }
    }
    private void GetItem(RaycastHit2D hit)
    {
        if (gameControl.isEditable) return;
        ItemInfo iteminfo = hit.transform.GetComponent<ItemInfo>();
        inventory.AcquireItem(iteminfo.item,iteminfo.quantity);
        collections.Collect(iteminfo.item);
        Destroy(hit.transform.gameObject, 0.1f);
        ShowGetEffect(hit.transform, iteminfo.item, iteminfo.quantity);
    }

    private void GetDust(RaycastHit2D hit)
    {
        hit.transform.GetComponent<Dust>().ShowMenu(true);
    }

    public void PleaseRemoveDust(Transform _transform)
    {
        StartCoroutine(RemoveDust(_transform));
    }

    IEnumerator RemoveDust(Transform _transform)
    {
        characterCtrl.GetComponent<Transform>().position = _transform.position + Vector3.up * 0.01f + Vector3.right * 0.3f;
        characterCtrl.isReaction = true;
        int rand = Random.Range(0, 5);
        if (rand == 0)
        {
            characterCtrl.ChangeAnimation("깜짝");
            characterCtrl.Talk("세상에! 이 먼지 좀 봐!");
        }
        else if (rand == 1)
        {
            characterCtrl.ChangeAnimation("깜짝");
            characterCtrl.Talk("조금만 기다려! 내가 금방 치워줄게~!");
        }
        else if (rand == 2)
        {
            characterCtrl.ChangeAnimation("안녕");
            characterCtrl.Talk("이정도 쯤이야!");
        }
        else if (rand == 3)
        {
            characterCtrl.ChangeAnimation("안녕");
            characterCtrl.Talk("영차영차");
        }
        else if (rand == 4)
        {
            characterCtrl.ChangeAnimation("안녕");
            characterCtrl.Talk("즐거운 청소시간~!");
        }
        yield return new WaitForSeconds(2f);
        characterCtrl.ChangeAnimation("대기");
        Dust dust = _transform.GetComponent<Dust>();
        Destroy(_transform.gameObject, 0.1f);
        GameManager.gameManager.IncreaseExp(dust.Exp);
        ShowGetEffect(_transform, Energy.item, (int)-dust.Energy, Exp.item, (int)dust.Exp);
        characterCtrl.UpdateEnergyBar(-dust.Energy);
        characterCtrl.isReaction = false;
        characterCtrl.StartCoroutine("ChooseAction");

    }

    private void GetFurniture(RaycastHit2D hit)
    {
        Building furniture = hit.transform.GetComponent<Building>();
        if (gameControl.isEditable)
        {
            if (GridBuildingSystem.gbSystem.isOnMouse && GridBuildingSystem.gbSystem.temp == furniture)
            {
                furniture.ShowControlPanel(true);
            }
            else if(!GridBuildingSystem.gbSystem.isOnMouse)
            {
                furniture.ShowControlPanel(true);
            }
        }
        //if(furniture.Placed)
        //{
        //    furniture.ShowControlPanel(true);
        //    //if (furniture.CanBePlaced())
        //    //{
        //    //    furniture.PlaceFurniture();
        //    //}
        //}
        //else
        //{
        //    if (gameControl.isEditable)
        //    {
        //        if (GridBuildingSystem.gbSystem.isOnMouse) return;
        //       // GridBuildingSystem.gbSystem.RearrangeBuilding(hit.transform.gameObject);
        //    }

        //}    
    }


    private void ShowGetEffect(Transform tr, Item item, int quantity = 1)
    {
        GameObject obj = Instantiate(getEffectPrefab, tr.position, tr.rotation);
        obj.GetComponent<CsScore>().ChangeInfo(item, quantity);
    }

    private void ShowGetEffect(Transform tr, Item item, int quantity, Item item2, int quantity2)
    {
        GameObject obj = Instantiate(getEffectPrefab, tr.position, tr.rotation);
        obj.GetComponent<CsScore>().ChangeInfo(item, quantity);

        Transform tr2 = tr;
        tr2.position = new Vector3(tr2.position.x, tr2.position.y - 0.6f, tr2.position.z);
        GameObject obj2 = Instantiate(getEffectPrefab, tr2.position, tr2.rotation);
        obj2.GetComponent<CsScore>().ChangeInfo(item2, quantity2);
    }
}
