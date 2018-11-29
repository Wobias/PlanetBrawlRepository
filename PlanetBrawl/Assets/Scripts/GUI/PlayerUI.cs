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

    private float _p1dmg = 0f;
    private float _p2dmg = 0f;
    private float _p3dmg = 0f;
    private float _p4dmg = 0f;

    private Player_HealthController _p1HealthController;
    private Player_HealthController _p2HealthController;
    private Player_HealthController _p3HealthController;
    private Player_HealthController _p4HealthController;

    public List<GameObject> player1HealthBars = new List<GameObject>();
    public List<GameObject> player2HealthBars = new List<GameObject>();
    public List<GameObject> player3HealthBars = new List<GameObject>();
    public List<GameObject> player4HealthBars = new List<GameObject>();

    public List<GameObject> p1KillCountUI = new List<GameObject>();
    public List<GameObject> p2KillCountUI = new List<GameObject>();
    public List<GameObject> p3KillCountUI = new List<GameObject>();
    public List<GameObject> p4KillCountUI = new List<GameObject>();

    private List<GameObject> players = new List<GameObject>();

    // Use this for initialization
    public void InitUI(GameObject[] newPlayers)
    {
        players.AddRange(newPlayers);
        SortPlayers(players);

        if (_player1 != null)
        {
            ActivateHealthBars(0);
        }
        if (_player2 != null)
        {
            ActivateHealthBars(1);
        }
        if (_player3 != null)
        {
            ActivateHealthBars(2);
        }
        if (_player4 != null)
        {
            ActivateHealthBars(3);
        }

        hp1Material.color = colors[0];
        hp2Material.color = colors[0];
        hp3Material.color = colors[0];
        hp4Material.color = colors[0];

        DeactivateKillCount(p1KillCountUI);
        DeactivateKillCount(p2KillCountUI);
        DeactivateKillCount(p3KillCountUI);
        DeactivateKillCount(p4KillCountUI);

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
            CheckHealth(_p1HealthController, player1HealthBars, _p1HealthController.health, hp1Material);
        }
        if (_player2 != null)
        {
            CheckHealth(_p2HealthController, player2HealthBars, _p2HealthController.health, hp2Material);
        }
        if (_player3 != null)
        {
            CheckHealth(_p3HealthController, player3HealthBars, _p3HealthController.health, hp3Material);
        }
        if (_player4 != null)
        {
            CheckHealth(_p4HealthController, player4HealthBars, _p4HealthController.health, hp4Material);
        }
    }

    public void SetKillCount(int playerNumber, int killCount)
    {
        switch (playerNumber)
        {
            case 0:
                ActivateKillCount(killCount, p1KillCountUI);
                break;
            case 1:
                ActivateKillCount(killCount, p2KillCountUI);
                break;
            case 2:
                ActivateKillCount(killCount, p3KillCountUI);
                break;
            case 3:
                ActivateKillCount(killCount, p4KillCountUI);
                break;
            default:
                break;
        }
    }

    private void ActivateKillCount(int kc, List<GameObject> kcSymbols)
    {
        if (kc <= 0)
        {
            for (int i = 0; i < kcSymbols.Count; i++)
            {
                kcSymbols[i].SetActive(false);
            }
        }
        else if (kc <= 1)
        {
            kcSymbols[0].SetActive(true);
        }
        else if (kc <= 2)
        {
            kcSymbols[1].SetActive(true);
        }
        else if (kc <= 3)
        {
            kcSymbols[2].SetActive(true);
        }
    }

    private void DeactivateKillCount(List<GameObject> kCSymbols)
    {
        for (int i = 0; i < kCSymbols.Count; i++)
        {
            kCSymbols[i].SetActive(false);
        }

    }

    private void DeactivateHealthBars(List<GameObject> healthBars, float hP)
    {
        healthBars[Mathf.RoundToInt(hP)].SetActive(false);
        hP++;
    }

    private void CheckHealth(Player_HealthController planet_HealthController, List<GameObject> hpBars, float playerHealth, Material playerMaterial)
    {
        if (planet_HealthController.health <= 4)
        {
            DeactivateHealthBars(hpBars, playerHealth);
            playerMaterial.color = colors[1];
        }
        if (planet_HealthController.health <= 3)
        {
            DeactivateHealthBars(hpBars, playerHealth);
            playerMaterial.color = colors[2];
        }
        if (planet_HealthController.health <= 2)
        {
            DeactivateHealthBars(hpBars, playerHealth);
            playerMaterial.color = colors[3];
        }
        if (planet_HealthController.health <= 1)
        {
            DeactivateHealthBars(hpBars, playerHealth);
        }
        if (planet_HealthController.health <= 0)
        {
            DeactivateHealthBars(hpBars, playerHealth);
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

    public void ActivateHealthBars(int playerNumber)
    {
        switch (playerNumber)
        {
            case 0:
                for (int i = 0; i < player1HealthBars.Count; i++)
                {
                    player1HealthBars[i].SetActive(true);
                    hp1Material.color = colors[0];
                }
                break;
            case 1:
                for (int i = 0; i < player2HealthBars.Count; i++)
                {
                    player2HealthBars[i].SetActive(true);
                    hp2Material.color = colors[0];
                }
                break;
            case 2:
                for (int i = 0; i < player3HealthBars.Count; i++)
                {
                    player3HealthBars[i].SetActive(true);
                    hp3Material.color = colors[0];
                }
                break;
            case 3:
                for (int i = 0; i < player4HealthBars.Count; i++)
                {
                    player4HealthBars[i].SetActive(true);
                    hp4Material.color = colors[0];
                }
                break;
            default:
                break;
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
}
