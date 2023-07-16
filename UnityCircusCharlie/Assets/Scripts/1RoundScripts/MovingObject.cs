using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public GameObject gameObject = default;
    private float speed = default;

    // Start is called before the first frame update
    void Start()
    {
        gameObject = GameObject.Find("Player");

        speed = gameObject.GetComponent<PlayerController>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isGameOver == false)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }
}
