using UnityEngine;

public class Door : MonoBehaviour
{
    public GameModes[] supportedModes;

    private Collider2D doorCollider;
    private Collider2D portalTrigger;
    private Animator anim;


    private void Awake()
    {
        doorCollider = GetComponent<Collider2D>();
        portalTrigger = transform.GetChild(0).GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    public void SetState(GameModes newMode, bool open)
    {
        if (open)
        {
            open = false;

            for (int i = 0; i < supportedModes.Length; i++)
            {
                if (newMode == supportedModes[i])
                {
                    open = true;
                    break;
                }
            }
        }
        

        if (open)
        {
            doorCollider.enabled = false;
            portalTrigger.enabled = true;
            anim.SetBool("Open", true);
        }
        else
        {
            doorCollider.enabled = true;
            portalTrigger.enabled = false;
            anim.SetBool("Open", false);
        }
    }
}
