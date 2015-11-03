using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour
{
    SpriteRenderer spriteR;
    Texture2D texture;
    int height, width;
    Color col;
    Rect rect;
    Vector2 vec;

	void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        texture = Instantiate(Resources.Load("Texture/Sol") as Texture2D);
        height = texture.height;
        width = texture.width;
        rect = new Rect(0, 0, width, height);
        vec = new Vector2(0, 0);
	}

	void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
			DigByPoint(Random.Range(0, height), Random.Range(0, width));
	}

    public void DigByPoint(int inputx, int inputy)
    {
        for(int Xc = inputx -30; Xc < inputx + 30; Xc++)
        {
			for(int Yc = inputy - 30; Yc < inputy + 30; Yc++)
            {
				col = texture.GetPixel(Xc, Yc);
				if(col.a != 0f)
				{
            		col = new Color(0, 0, 0, 0.0f);
                	texture.SetPixel(Xc, Yc, col);
				}
            }
        }

        texture.Apply(false);

        spriteR.sprite = Sprite.Create(texture, rect, vec, 128);
    }
}
