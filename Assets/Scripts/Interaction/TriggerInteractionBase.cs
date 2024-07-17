using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInteractionBase : MonoBehaviour, IInteractable
{
    public GameObject Player { get ; set ; }
    public bool IsInteractable { get ; set ; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (IsInteractable)
        {
            if (InputManager.Interact)
            {
                Interact();
            }
        }
    }
    public virtual void Interact()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == Player)
        {
            IsInteractable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == Player)
        {
            IsInteractable = false;
        }
    }
}
