using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    Texture2D background;
    Color[] colorData;
    int width = 320;
    int height = 200;
    public MeshRenderer planeRenderer;
    // Start is called before the first frame update
    private Sprite sprite;
    private GameObject go;
    void Start()
    {
        colorData = new Color[width * height];
        for (int i = 0; i < colorData.Length; i++)
        {
            colorData[i] = Color.red;
        }
        background = new Texture2D(width, height);
        background.SetPixels(colorData);
        sprite = Sprite.Create(background, new Rect(0, 0, width, height), Vector2.zero);
        
        go = new GameObject();
        go.AddComponent<SpriteRenderer>();
        go.GetComponent<SpriteRenderer>().sprite = sprite;
        go.GetComponent<SpriteRenderer>().material.SetTexture("Texture", background);
        go.transform.SetParent(gameObject.transform);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
