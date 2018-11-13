using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillTrigger : MonoBehaviour
{
    KotH_ManagerScript koth_manager;
    public static HillTrigger hillTrigger;

    public GameObject hill;

    public float hillTimer = 8f;
    private float hillResetTimer = 10f;

    public bool hillIsOpen = false;

    Vector3 startSize;
    Vector3 minSizeVector;
    public float minSize;

    Vector2 hillPosition;

    SpriteRenderer rend;

    void Start()
    {
        koth_manager = FindObjectOfType<KotH_ManagerScript>();
        hillTrigger = this;
        minSizeVector = new Vector3(minSize, minSize, minSize);

        hill.transform.position = new Vector2(0, 0);

        startSize = transform.localScale;
        rend = GetComponent<SpriteRenderer>();
        //rend.color = Color.green;        
    }

    // Update is called once per frame
    void Update()
    {
        if (hillTimer <= hillResetTimer / 2)
            transform.localScale = Vector3.Lerp(transform.localScale, minSizeVector, Time.deltaTime / hillTimer);

        hillTimer -= Time.deltaTime;

        if (hillTimer <= 0f)
        {
            hillIsOpen = false;
            hillResetTimer = Random.Range(5f, 15f);
            hillTimer = hillResetTimer;
            SetPortal();
        }
    }

    void SetPortal()
    {
        //Reichweite anpassen
        hillPosition.x = Random.Range(-18f, 19f);
        hillPosition.y = Random.Range(-8f, 9f);

        hill.transform.position = hillPosition;
        hill.transform.localScale = startSize;
        hill.SetActive(true);

        hillIsOpen = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        koth_manager.PlayerEnter(other.gameObject.layer);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        koth_manager.PlayerExit(other.gameObject.layer);
    }
}
