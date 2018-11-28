using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterPlanet : MonoBehaviour
{
    private float fadeOutTimer;

    private bool hasExploded = false;

    public GameObject explosion;

    public GameObject planetPartLeft;
    public GameObject planetPartRight;
    public GameObject planetPartBottom;

    private Rigidbody2D rbLeft;
    private Rigidbody2D rbRight;
    private Rigidbody2D rbBottom;

    private Vector2 directionLeft;
    private Vector2 directionRight;
    private Vector2 directionBottom;

    private SpriteRenderer leftSprite;
    private SpriteRenderer rightSprite;
    private SpriteRenderer bottomSprite;

    private void Start()
    {
        leftSprite = planetPartLeft.GetComponent<SpriteRenderer>();
        rightSprite = planetPartRight.GetComponent<SpriteRenderer>();
        bottomSprite = planetPartBottom.GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        hasExploded = false;
        fadeOutTimer = 1.2f;
        rbLeft = planetPartLeft.GetComponent<Rigidbody2D>();
        rbRight = planetPartRight.GetComponent<Rigidbody2D>();
        rbBottom = planetPartBottom.GetComponent<Rigidbody2D>();

        //Resetting Components
        planetPartLeft.transform.position = transform.position;
        planetPartLeft.transform.rotation = transform.rotation;
        planetPartRight.transform.position = transform.position;
        planetPartRight.transform.rotation = transform.rotation;
        planetPartBottom.transform.position = transform.position;
        planetPartBottom.transform.rotation = transform.rotation;

        leftSprite.color = new Color(1f, 1f, 1f, 1f);
        rightSprite.color = new Color(1f, 1f, 1f, 1f);
        bottomSprite.color = new Color(1f, 1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartCoroutine("Explode");
        }
        if (hasExploded == true)
        {
            planetPartLeft.transform.Rotate(Vector3.back * Random.Range(80f, 150f) * Time.deltaTime, Space.Self);
            planetPartRight.transform.Rotate(Vector3.forward * Random.Range(80f, 150f) * Time.deltaTime, Space.Self);
            planetPartBottom.transform.Rotate(Vector3.forward * Random.Range(80f, 150f) * Time.deltaTime, Space.Self);

            fadeOutTimer -= Time.deltaTime;
            leftSprite.color = new Color(1f, 1f, 1f, fadeOutTimer);
            rightSprite.color = new Color(1f, 1f, 1f, fadeOutTimer);
            bottomSprite.color = new Color(1f, 1f, 1f, fadeOutTimer);
        }
    }

    private IEnumerator Explode()
    {
        if (!hasExploded)
        {
            Instantiate(explosion, transform.position, transform.rotation);

            if (planetPartLeft != null)
            {
                directionLeft.x = Random.Range(-5f, -1f);
                directionLeft.y = Random.Range(1f, 5f);
                rbLeft.mass = Random.Range(0.5f, 2f);
                rbLeft.AddForce(directionLeft * 1.5f , ForceMode2D.Impulse);

            }
            if (planetPartRight != null)
            {
                directionRight.x = Random.Range(1f, 5f);
                directionRight.y = Random.Range(1f, 5f);
                rbRight.mass = Random.Range(0.5f, 2f);
                rbRight.AddForce(directionRight * 1.5f, ForceMode2D.Impulse);
            }
            if (planetPartBottom != null)
            {
                directionBottom.x = Random.Range(-5f, 5f);
                directionBottom.y = Random.Range(-5f, -1f);
                rbBottom.mass = Random.Range(0.5f, 2f);
                rbBottom.AddForce(directionBottom * 1.5f, ForceMode2D.Impulse);
            }
            hasExploded = true;

            yield return new WaitForSecondsRealtime(1.5f);

            gameObject.SetActive(false);

            yield return null;
        }
    }
}
