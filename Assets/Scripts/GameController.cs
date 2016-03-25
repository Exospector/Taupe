using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    private GameObject mainSprite, upSprite, bottomSprite, character;
    private Transform mainSpriteTransform, characterTransform;
    private int spriteCycleNumber;

    public bool isGameActive;

    void Awake()
    {
        character = GameObject.Find("Taupe");
        mainSprite = GameObject.Find("SolSprite");

        characterTransform = character.GetComponent<Transform>();
    }

    void Start()
    {
        isGameActive = true;
        spriteCycleNumber = 0;

        // Initial upSprite creation
        upSprite = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/SolSprite"));
        upSprite.transform.position = mainSprite.transform.position + new Vector3(0, 8, 0);

        StartCoroutine("GenerateAndDestroy");
    }
	
	void Update()
    {
        
	}

    // Terrain generation and destruction coroutine
    IEnumerator GenerateAndDestroy()
    {
        while(isGameActive)
        {
            // Generate next terrain sprite
            if ((characterTransform.position.y - spriteCycleNumber * 8) % 8 > 4)
            {
                // Destroy previous terrain Sprite
                if(bottomSprite)
                {
                    GameObject.Destroy(bottomSprite);
                }

                // Waterfall Sprite transition
                bottomSprite = mainSprite;
                mainSprite = upSprite;

                // Instanciate new terrain
                upSprite = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/SolSprite"));
                upSprite.transform.position = mainSprite.transform.position + new Vector3(0, 8, 0);
                
                // Increment cycle number
                spriteCycleNumber++;
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

    public void StopGame()
    {
        isGameActive = false;
        StopCoroutine("GenerateAndDestroy");
    }

    public void ResumeGame()
    {
        isGameActive = true;
        StartCoroutine("GenerateAndDestroy");
    }
}