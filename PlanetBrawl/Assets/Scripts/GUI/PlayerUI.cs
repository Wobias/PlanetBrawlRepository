using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public List<Color> colors;

    public GameObject p1UI;
    public GameObject p2UI;
    public GameObject p3UI;
    public GameObject p4UI;

    public Image p1Portrait;
    public Image p2Portrait;
    public Image p3Portrait;
    public Image p4Portrait;

    public Sprite city;
    public Sprite desert;
    public Sprite earth;
    public Sprite gas;
    public Sprite ice;
    public Sprite lava;
    public Sprite ocean;
    public Sprite toxic;

    public Sprite portraitCity;
    public Sprite portraitDesert;
    public Sprite portraitEarth;
    public Sprite portraitGas;
    public Sprite portraitIce;
    public Sprite portraitLava;
    public Sprite portraitOcean;
    public Sprite portraitToxic;


    public Material hp1Material;
    public Material hp2Material;
    public Material hp3Material;
    public Material hp4Material;

    private GameObject _player1;
    private GameObject _player2;
    private GameObject _player3;
    private GameObject _player4;

    private float _p1dmg;
    private float _p2dmg;
    private float _p3dmg;
    private float _p4dmg;

    private Player_HealthController _p1HealthController;
    private Player_HealthController _p2HealthController;
    private Player_HealthController _p3HealthController;
    private Player_HealthController _p4HealthController;

    public List<GameObject> player1HealthBars = new List<GameObject>();
    public List<GameObject> player2HealthBars = new List<GameObject>();
    public List<GameObject> player3HealthBars = new List<GameObject>();
    public List<GameObject> player4HealthBars = new List<GameObject>();

    private List<GameObject> players = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        SortPlayers(players);
        ActivateHealthBars();
        hp1Material.color = colors[0];
        hp2Material.color = colors[0];
        hp3Material.color = colors[0];
        hp4Material.color = colors[0];

        _p1dmg = 0f;
        _p2dmg = 0f;
        _p3dmg = 0f;
        _p4dmg = 0f;

        if (_player1 != null)
        {
            SelectFaceSprite(_player1, p1Portrait);
            p1UI.SetActive(true);
        }
        else
        {
            p1UI.SetActive(false);
        }

        if (_player2 != null)
        {
            SelectFaceSprite(_player2, p2Portrait);
            p2UI.SetActive(true);
        }
        else
        {
            p2UI.SetActive(false);
        }

        if (_player3 != null)
        {
            SelectFaceSprite(_player3, p3Portrait);
            p3UI.SetActive(true);
        }
        else
        {
            p3UI.SetActive(false);
        }

        if (_player4 != null)
        {
            SelectFaceSprite(_player4, p4Portrait);
            p4UI.SetActive(true);
        }
        else
        {
            p4UI.SetActive(false);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (_player1 != null)
        {
            CheckHealth(_player1, _p1HealthController, player1HealthBars, _p1HealthController.health, hp1Material);
        }
        else
        {
            DeactivateAllHealthBars(player1HealthBars);
        }

        if (_player2 != null)
        {
            CheckHealth(_player2, _p2HealthController, player2HealthBars, _p2HealthController.health, hp2Material);
        }
        else
        {
            DeactivateAllHealthBars(player2HealthBars);
        }

        if (_player3 != null)
        {
            CheckHealth(_player3, _p3HealthController, player3HealthBars, _p3HealthController.health, hp3Material);
        }
        else
        {
            DeactivateAllHealthBars(player3HealthBars);
        }

        if (_player4 != null)
        {
            CheckHealth(_player4, _p4HealthController, player4HealthBars, _p4HealthController.health, hp4Material);
        }
        else
        {
            DeactivateAllHealthBars(player4HealthBars);
        }

    }

    private void DeactivateHealthBars(List<GameObject> healthBars, float dmg)
    {
        healthBars[Mathf.RoundToInt(dmg)].SetActive(false);
        dmg++;
    }

    private void CheckHealth(GameObject player, Player_HealthController player_HealthController, List<GameObject> hpBars, float damage, Material playerMaterial)
    {
        if (player_HealthController.health <= 4)
        {
            DeactivateHealthBars(hpBars, damage);
            playerMaterial.color = colors[1];
        }
        if (player_HealthController.health <= 3)
        {
            DeactivateHealthBars(hpBars, damage);
            playerMaterial.color = colors[2];
        }
        if (player_HealthController.health <= 2)
        {
            DeactivateHealthBars(hpBars, damage);
            playerMaterial.color = colors[3];
        }
        if (player_HealthController.health <= 1)
        {
            DeactivateHealthBars(hpBars, damage);
        }
        if (player_HealthController.health <= 0)
        {
            DeactivateHealthBars(hpBars, damage);
        }
    }

    private void SortPlayers(List<GameObject> playerslist)
    {
        for (int i = 0; i < playerslist.Count; i++)
        {
            if (playerslist[i].layer == 8)
            {
                _player1 = playerslist[i];
                _p1HealthController = playerslist[i].GetComponent<Player_HealthController>();
            }
            else if (playerslist[i].layer == 9)
            {
                _player2 = playerslist[i];
                _p2HealthController = playerslist[i].GetComponent<Player_HealthController>();
            }
            else if (playerslist[i].layer == 10)
            {
                _player3 = playerslist[i];
                _p3HealthController = playerslist[i].GetComponent<Player_HealthController>();
            }
            else if (playerslist[i].layer == 11)
            {
                _player4 = playerslist[i];
                _p4HealthController = playerslist[i].GetComponent<Player_HealthController>();
            }
        }
    }

    private void ActivateHealthBars()
    {
        for (int i = 0; i < player1HealthBars.Count; i++)
        {
            player1HealthBars[i].SetActive(true);
        }
        for (int i = 0; i < player2HealthBars.Count; i++)
        {
            player2HealthBars[i].SetActive(true);
        }
        for (int i = 0; i < player3HealthBars.Count; i++)
        {
            player3HealthBars[i].SetActive(true);
        }
        for (int i = 0; i < player4HealthBars.Count; i++)
        {
            player4HealthBars[i].SetActive(true);
        }
    }

    private void SelectFaceSprite(GameObject player, Image portrait)
    {
        if (player.GetComponentInChildren<SpriteRenderer>().sprite == city)
        {
            portrait.sprite = portraitCity;
        }
        if (player.GetComponentInChildren<SpriteRenderer>().sprite == desert)
        {
            portrait.sprite = portraitDesert;
        }
        if (player.GetComponentInChildren<SpriteRenderer>().sprite == earth)
        {
            portrait.sprite = portraitEarth;
        }
        if (player.GetComponentInChildren<SpriteRenderer>().sprite == gas)
        {
            portrait.sprite = portraitGas;
        }
        if (player.GetComponentInChildren<SpriteRenderer>().sprite == ice)
        {
            portrait.sprite = portraitIce;
        }
        if (player.GetComponentInChildren<SpriteRenderer>().sprite == lava)
        {
            portrait.sprite = portraitLava;
        }
        if (player.GetComponentInChildren<SpriteRenderer>().sprite == ocean)
        {
            portrait.sprite = portraitOcean;
        }
        if (player.GetComponentInChildren<SpriteRenderer>().sprite == toxic)
        {
            portrait.sprite = portraitToxic;
        }
    }

    private void DeactivateAllHealthBars(List<GameObject> hpBarList)
    {
        for (int i = 0; i < hpBarList.Count; i++)
        {
            hpBarList[i].SetActive(false);
        }
    }
}
