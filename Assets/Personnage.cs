using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Personnage : MonoBehaviour
{
	public Camera camera;
	public bool isDigging;
	GameObject go;
	float speed = 0.05f;


	void Start ()
	{
		go = gameObject;
		isDigging = true;

		StartCoroutine("DiggingCheck");
	}

	void Update ()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			if((Input.GetTouch (0).phase == TouchPhase.Stationary) || (Input.GetTouch (0).phase == TouchPhase.Moved && Input.GetTouch (0).deltaPosition.magnitude < 2))
			{
				Vector2 touchPosition = camera.ScreenToWorldPoint(Input.GetTouch (0).position);
				// Move object across XY plane
				transform.Translate (touchPosition.x * speed, touchPosition.y * speed, 0);
			}
		}
	}

	IEnumerator DiggingCheck()
	{
		int[] texCoord = new int[2];

		while(isDigging)
		{
			texCoord = getTextureCoord();
			GameObject.Find ("sol_child").GetComponent<Ground>().DigByPoint(texCoord[0], texCoord[1]);

			yield return new WaitForSeconds(0.1f);
		}
	}

	int[] getTextureCoord()
	{
		int[] result = new int[2];
		RaycastHit2D[] hits;
		Ray ray = Camera.main.ScreenPointToRay(go.transform.position);
		hits = Physics2D.RaycastAll(Camera.main.transform.position, go.transform.position, 100);

		for(int i=0; i<hits.Length; i++)
		{
			RaycastHit2D hit=hits[i];
			Transform sol= hit.transform;
			if(sol.gameObject.tag == "Sol")
			{
				Vector2 vec = sol.position;
				Vector2 vecPers= go.transform.position;
				vec = vec + vecPers;
				vec *= 128;
				result[0] = (int)vec.x;
				result[1] = (int)vec.y;
			}
		}

		return result;
	}
}