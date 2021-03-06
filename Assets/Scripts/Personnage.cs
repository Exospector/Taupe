using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Personnage : MonoBehaviour
{
	public bool isDigging;
	GameObject go;
	float speed = 0.05f;
    ushort platform=0;


	void Start ()
	{
		if (Application.platform == RuntimePlatform.Android)
		{
			platform = 1;
		}
		go = gameObject;
		isDigging = true;

		StartCoroutine("DiggingCheck");
	}

	void Update ()
	{

		if (platform == 1)
		{
			if((Input.GetTouch (0).phase == TouchPhase.Stationary) || (Input.GetTouch (0).phase == TouchPhase.Moved && Input.GetTouch (0).deltaPosition.magnitude < 2))
			{
				Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch (0).position);
				touchPosition.x=touchPosition.x-transform.position.x;
				touchPosition.y=touchPosition.y-transform.position.y;
				// Move object across XY plane
				transform.Translate (touchPosition.x * speed, touchPosition.y * speed, 0);
				Vector3 vec = new Vector3 (transform.position.x, transform.position.y,Camera.main.transform.position.z);
			}
		}
	}

	IEnumerator DiggingCheck()
	{
		int[] texCoord = new int[2];
        Ground sol = GameObject.Find ("sol_child").GetComponent<Ground>();

		while(isDigging)
		{
			texCoord = getTextureCoord();
			sol.DigByPoint(texCoord[0]/64, texCoord[1]/64);

			yield return new WaitForSeconds(0.05f);
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
                Vector2 vecPers = go.transform.position;
				vec = vec + vecPers;
				vec *= 128;
				result[0] = (int)vec.x;
				result[1] = (int)vec.y;
			}
		}

		return result;
	}
}