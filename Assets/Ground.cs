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
    MaterialPropertyBlock block;

	void Start()
    {
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        texture = Instantiate(Resources.Load("Texture/Sol") as Texture2D);
        height = texture.height;
        width = texture.width;
        rect = new Rect(0, 0, width, height);
        vec = new Vector2(0, 0);
        block = new MaterialPropertyBlock();
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

        /*for(int iteratory = inputy - 30; iteratory < inputy + 30; iteratory++)
        {
            for (int iteratorx = inputx - 30; iteratorx < inputx + 30; iteratorx++)
            {
                borderValueTest = iteratory * 1024 + iteratorx;
                if(borderValueTest >= 0)
                {
                    temp = iteratory * 1024 + iteratorx;
                    if(!(colors[temp].a == 0.0f))
                    {
                        colors[temp] = new Color32(0, 0, 0, 0);
                    }
                }
            }
        }*/

        int d = (5 - 30 * 4) / 4;
        int x = 0;
        int y = 30;
        Color color = new Color32(0, 0, 0, 0); ;
 
        do
        {
            temp = y * 1024 + x;
            // ensure index is in range before setting (depends on your image implementation)
            // in this case we check if the pixel location is within the bounds of the image before setting the pixel
            if (inputx + x >= 0 && inputx + x <= 1024 - 1 && inputy + y >= 0 && inputy + y <= 1024 - 1) colors[inputx + inputy * 1024 + x + y * 1024] = color;
            if (inputx + x >= 0 && inputx + x <= 1024 - 1 && inputy - y >= 0 && inputy - y <= 1024 - 1) colors[inputx + inputy * 1024 + x - y * 1024] = color;
            if (inputx - x >= 0 && inputx - x <= 1024 - 1 && inputy + y >= 0 && inputy + y <= 1024 - 1) colors[inputx + inputy * 1024 - x + y * 1024] = color;
            if (inputx - x >= 0 && inputx - x <= 1024 - 1 && inputy - y >= 0 && inputy - y <= 1024 - 1) colors[inputx + inputy * 1024 - x - y * 1024] = color;
            
            if (d < 0)
            {
                d += 2 * x + 1;
            }
            else
            {
                d += 2 * (x - y) + 1;
                y--;
            }
            x++;
        } while (x <= y);

        texture.SetPixels32(colors);
        texture.Apply(false);

        block.AddTexture("_MainTex", texture);
        spriteR.SetPropertyBlock(block);
    }
}
