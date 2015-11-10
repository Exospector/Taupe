using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour
{
    SpriteRenderer spriteR;
    Texture2D texture;
    int height, width, temp;
    Color col;
    Rect rect;
    Vector2 vec;
    Color32[] colors;

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
        colors = texture.GetPixels32();
        int borderValueTest;

        for(int iteratory = inputy - 30; iteratory < inputy + 30; iteratory++)
        {
            for (int iteratorx = inputx - 30; iteratorx < inputx + 30; iteratorx++)
            {
                borderValueTest = iteratory * 1024 + iteratorx;
                if(borderValueTest >= 0)
                {
                    temp = iteratory * 1024 + iteratorx;
                    if(!(colors[temp].a == 0.0f))
                    {
                        colors[temp] = new Color(0, 0, 0, 0.0f);
                    }
                }
            }
        }

        texture.SetPixels32(colors);

        texture.Apply(false);

        spriteR.sprite = Sprite.Create(texture, rect, vec, 128);
    }
}
