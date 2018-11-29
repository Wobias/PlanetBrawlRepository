using UnityEngine;

public class Door : MonoBehaviour
{
    public GameModes[] supportedModes;

    private Collider2D doorCollider;
    private Animator anim;


    private void Start()
    {
        doorCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        SetState(GameModes.deathmatch);
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
            anim.SetBool("Open", true);
        }
        else
        {
            doorCollider.enabled = true;
            anim.SetBool("Open", false);
        }
    }
}
