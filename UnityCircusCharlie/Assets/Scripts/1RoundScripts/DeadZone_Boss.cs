using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone_Boss : MonoBehaviour
{
    public GameObject laser;
    private float duration = 2f;

    private Vector3 initialPosition;    // 시작 지점
    private Vector3 targetPosition;     // 목표 지점
    private float elapsedTime = 0f;     // 경과 시간

    private bool laserCheck = false;
    private bool moveCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        StartCoroutine(DelayedAction());
    }

    // Update is called once per frame
    void Update()
    {
        if (laserCheck == false && moveCheck == true)
        {
            laserCheck = true;
            GameObject gameObjectLaser = Instantiate(laser, targetPosition, Quaternion.identity);
            Destroy(gameObjectLaser, 0.6f);
        }
    }

    IEnumerator DelayedAction()
    {
        while (true)
        {
            if (moveCheck == false)
            {
                int count = Random.Range(0, 2);

                if (count == 0)
                {
                    targetPosition = new Vector3(8f, -3f, 0f);
                }
                else if (count == 1)
                {
                    targetPosition = new Vector3(8f, -1f, 0f);
                }

                elapsedTime = 0f;

                while (elapsedTime <= duration)
                {
                    float time = elapsedTime / duration;
                    transform.position = Vector3.Lerp(initialPosition, targetPosition, time);

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                moveCheck = true;
            }
            else if (moveCheck == true)
            {
                elapsedTime = 0f;

                while (elapsedTime <= duration)
                {
                    float time = elapsedTime / duration;
                    transform.position = Vector3.Lerp(targetPosition, initialPosition, time);
                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                moveCheck = false;
            }

            laserCheck = false;

            yield return new WaitForSeconds(2f);
        }
    }
}