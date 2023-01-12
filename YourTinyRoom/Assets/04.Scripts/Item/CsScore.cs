using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CsScore : MonoBehaviour
{
	private Vector3 pos = Vector3.zero;
	public float ScoreDelay = 0.1f;
	private CanvasGroup cg;
	public Image getItemImage;
	public Text getItemQuantity;
	public Item Exp;
	public Item Gold;
	public Item Crystal;
	void Start()
	{
		cg = GetComponent<CanvasGroup>();
		pos = transform.position;
		StartCoroutine("DisplayScore");
	}

	void Update()
	{		
		pos.y += 0.0012f;
		transform.position = pos;
	}
	IEnumerator DisplayScore()
	{
		yield return new WaitForSeconds(ScoreDelay);

		for (float a = 1; a >= 0; a -= 0.2f)
		{
			cg.alpha = a;
			yield return new WaitForSeconds(0.1f);
		}
		Destroy(this.gameObject, 0.1f);
	}
	//public void ChangeInfo(ItemInfo itemInfo, float quantity)
	//{
	//	getItemImage.sprite = itemInfo.item.itemImage;
	//	getItemQuantity.text = "+" + quantity.ToString();
	//}
	public void ChangeInfo(Item item, int quantity)
    {
		getItemImage.sprite = item.itemImage;
		getItemImage.type = 0;
		getItemImage.preserveAspect = true;
		if (quantity < 0)
		{
			getItemQuantity.text = quantity.ToString();
		}
		else
		{
			getItemQuantity.text = "+" + quantity.ToString();
		}
	}
}
