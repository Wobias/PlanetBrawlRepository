using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameModeSelection : MonoBehaviour
{
    public string[] modeNames;
    public Sprite[] modeSprites;
    public Door[] lobbyDoors;
    public TextMeshProUGUI gmText;

    private SpriteRenderer spriteRenderer;
    private int selection = 0;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        selection++;

        if (selection >= modeSprites.Length)
            selection = 0;

        spriteRenderer.sprite = modeSprites[selection];
        GameManager.instance.gameMode = (GameModes)selection;
        gmText.text = modeNames[selection];

        for (int i = 0; i < lobbyDoors.Length; i++)
        {
            lobbyDoors[i].SetState((GameModes)selection);
        }
    }
}
