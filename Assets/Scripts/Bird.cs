using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private static Rigidbody2D baseRB;
    private const float jumpFloat = 100f;

    private void Awake()
    {
        baseRB = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public static void Jump()
    {
        baseRB.GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpFloat;
    }


} 
