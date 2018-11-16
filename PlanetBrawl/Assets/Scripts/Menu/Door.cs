using UnityEngine;

public class Door : MonoBehaviour
{
    public GameModes[] supportedModes;

    private Collider2D doorCollider;
    private MeshRenderer meshRenderer;


    private void Start()
    {
        doorCollider = GetComponent<Collider2D>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetState(GameModes newMode)
    {
        bool newOpen = false;

        for (int i = 0; i < supportedModes.Length; i++)
        {
            if (newMode == supportedModes[i])
            {
                newOpen = true;
                break;
            }
        }

        if (newOpen)
        {
            doorCollider.enabled = false;
            meshRenderer.enabled = false;
        }
        else
        {
            doorCollider.enabled = true;
            meshRenderer.enabled = true;
        }
    }
}
