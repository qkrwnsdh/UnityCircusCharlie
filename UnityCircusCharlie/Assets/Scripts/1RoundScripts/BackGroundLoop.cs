using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    private float width;

    private void Awake()
    {
        BoxCollider2D backGroundCollider = GetComponent<BoxCollider2D>();
        width = backGroundCollider.size.x;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -width)
        {
            Reposition_R();
        }
        if (transform.position.x >= width)
        {
            Reposition_L();
        }
    }

    private void Reposition_R()
    {
        Vector2 offset = new Vector2(width * 2f, 0f);
        transform.position = transform.position.AddVector(offset);
    }

    private void Reposition_L()
    {
        Vector2 offset = new Vector2(-width * 2f, 0f);
        transform.position = transform.position.AddVector(offset);
    }
}
