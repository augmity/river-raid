using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    Texture2D background;

    static int width = 320;
    static int height = 200;

    public MeshRenderer planeRenderer;
    private Sprite sprite;
    private GameObject go;

    private static int riverPosition = width / 2;
    private static int riverWidth = width / 3;
    private static int shoreWidth = width / 50;

    private static int minRiverX = width / 5;
    private static int maxRiverX = width - minRiverX;

    private static Color grass = new Color(.05f, .75f, .0f);
    private static Color water = new Color(.0f, .15f, 1f);
    private static Color shore = new Color(.55f, .51f, .30f);

    // TODO scrolling, multiple tiles with swapping
    void Start()
    {
        var firstTile = CreateTile();
        background = new Texture2D(width, height);
        background.SetPixels(firstTile);
        background.filterMode = FilterMode.Point;
        background.Apply();
        sprite = Sprite.Create(background, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 100.0f);

        go = new GameObject();
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>() as SpriteRenderer;
        sr.sprite = sprite;
        sr.material.SetTexture("Texture", background);
        go.transform.SetParent(gameObject.transform);

        // Stretch sprite to full camera
        go.transform.localScale = new Vector3(1, 1, 1);
        var w = sr.sprite.bounds.size.x;
        var h = sr.sprite.bounds.size.y;
        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        go.transform.localScale = new Vector2((float)(worldScreenWidth / w), (float)(worldScreenHeight / h));
    }

    // Update is called once per frame
    void Update()
    {

    }

    //TODO  Make tendency so that it more smoothly moves left/right a bit further at a time
    //      Add seeded randomness, so the game is the same every time.
    //      Add splitting, and narrowing/widening
    private Color[] CreateTile() {
        Color color;
        Color[,] colors = new Color[height, width];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x > riverPosition - riverWidth / 2 && x < riverPosition + riverWidth / 2)
                {
                    color = water;
                }
                else if(x > riverPosition - riverWidth / 2 - shoreWidth && x < riverPosition + riverWidth / 2 + shoreWidth){
                    color = shore;
                }
                else
                {
                    color = grass;
                }
                colors[y, x] = color;
            }
            riverPosition += UnityEngine.Random.Range(-2, 3);
            riverPosition = (int)Mathf.Clamp(riverPosition, minRiverX, maxRiverX);
        }
        var arr = new List<Color>();
        foreach (var item in colors)
        {
            arr.Add(item);
        }
        return arr.ToArray();
    }
}
