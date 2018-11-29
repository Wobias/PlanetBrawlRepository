using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterPlanet : MonoBehaviour
{
    private float fadeOutTimer;

    private bool hasExploded = false;

    public GameObject planet;

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

    private Color leftColor;
    private Color rightColor;
    private Color bottomColor;

    private Transform myTransform;

    private void Start()
    {
        myTransform = GetComponent<Transform>();

        planet = myTransform.root.gameObject;

        rbLeft = planetPartLeft.GetComponent<Rigidbody2D>();
        rbRight = planetPartRight.GetComponent<Rigidbody2D>();
        rbBottom = planetPartBottom.GetComponent<Rigidbody2D>();

        if (leftColor != null)
        {
            leftColor = planetPartLeft.GetComponent<SpriteRenderer>().color;
        }
        if (rightColor != null)
        {
            rightColor = planetPartRight.GetComponent<SpriteRenderer>().color;
        }
        if (bottomColor != null)
        {
            bottomColor = planetPartBottom.GetComponent<SpriteRenderer>().color;
        }
    }

    void OnEnable()
    {
        hasExploded = false;
        fadeOutTimer = 1f;

        //Resetting Components
        ResetComponents(planetPartLeft, leftColor);
        ResetComponents(planetPartRight, rightColor);
        ResetComponents(planetPartBottom, bottomColor);
    }

    // Update is called once per frame
    void Update()
    {

        if (hasExploded == true)
        {
            planetPartLeft.transform.Rotate(Vector3.back * Random.Range(80f, 150f) * Time.deltaTime, Space.Self);
            planetPartRight.transform.Rotate(Vector3.forward * Random.Range(80f, 150f) * Time.deltaTime, Space.Self);
            planetPartBottom.transform.Rotate(Vector3.forward * Random.Range(80f, 150f) * Time.deltaTime, Space.Self);

            StartCoroutine("FadeSprite");

        }

        if (hasExploded == false)
        {
            StartCoroutine("Explode");
        }
    }

    private IEnumerator Explode()
    {
        GameObject particle = Instantiate(explosion, transform.position, transform.rotation);

        //transform.parent = null;

        if (planetPartLeft != null)
        {
            directionLeft.x = Random.Range(-5f, -1f);
            directionLeft.y = Random.Range(1f, 5f);
            rbLeft.mass = Random.Range(0.5f, 2f);
            rbLeft.AddForce(directionLeft * 1.5f, ForceMode2D.Impulse);
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

        myTransform.parent = planet.transform;
        myTransform.position = planet.transform.position;

        ResetComponents(planetPartLeft, leftColor);
        ResetComponents(planetPartRight, rightColor);
        ResetComponents(planetPartBottom, bottomColor);

        Destroy(particle);

        gameObject.SetActive(false);

        yield return null;
    }

    private void ResetComponents(GameObject planetPart, Color sprite)
    {
        planetPart.transform.rotation = transform.rotation;
        planetPart.transform.position = transform.position;

        if (sprite != null)
        {
            sprite = new Color(1f, 1f, 1f, 1f);
        }
    }

    private IEnumerator FadeSprite()
    {
        Color fadeColor = leftColor;
        while (fadeColor.a > 0)
        {
            fadeColor.a -= 0.1f ;
            leftColor = fadeColor;
            rightColor = fadeColor;
            bottomColor = fadeColor;
            Debug.Log("color fade");
        }
        yield return null;
    }
}

