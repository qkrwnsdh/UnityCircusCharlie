using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public GameObject obstacles;

    private bool stepped = false;

    private void OnEnable()
    {
        stepped = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag.Equals("Player") && stepped == false)
        {
            stepped = true;
            GameManager.instance.AddScore(1);
        }
    }
}
