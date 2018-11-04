using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeSelection : MonoBehaviour
{
    public Sprite ffaSprite;
    public Sprite teamSprite;

    private MenuManager manager;
    private SpriteRenderer spriteRenderer;
    private bool teamMode = false;


    private void Start()
    {
        manager = FindObjectOfType<MenuManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        teamMode = !teamMode;
        manager.SwitchTeamMode(teamMode);

        if (teamMode)
        {
            spriteRenderer.sprite = teamSprite;
        }
        else
        {
            spriteRenderer.sprite = ffaSprite;
        }
    }
}
