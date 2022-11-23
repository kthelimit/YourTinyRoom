using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CsScore : MonoBehaviour
{
	private Vector3 pos = Vector3.zero;
	public float ScoreDelay = 0.5f;
	private CanvasGroup cg;
	void Start()
	{
		cg = GetComponent<CanvasGroup>();
		pos = transform.position;
		StartCoroutine("DisplayScore");
	}

	void Update()
	{		
		pos.y += 0.12f;
		transform.position = pos;
	}
	IEnumerator DisplayScore()
	{
		yield return new WaitForSeconds(ScoreDelay);

		for (float a = 1; a >= 0; a -= 0.2f)
		{
			cg.alpha = a;
			yield return new WaitForFixedUpdate();
		}

		//Destroy(gameObject);
	}
}
