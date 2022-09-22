using System;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private static Rigidbody2D baseRB;
    private const float jumpFloat = 100f;
    public static Bird instance;
    public static Bird getInstance() { return instance; }
    public event EventHandler onDied;
    public event EventHandler onStart;

    private State state;
    private enum State
    {
        Waiting,
        Playing,
        Dead
    }

    private void Awake()
    {
        instance = this;
        baseRB = GetComponent<Rigidbody2D>();
        state = State.Waiting;
        baseRB.bodyType = RigidbodyType2D.Static;
    }
    public void Update()
    {
        switch (state)
        {
            default:
            case State.Waiting:
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    if (onStart != null) onStart(this, EventArgs.Empty);
                    baseRB.bodyType = RigidbodyType2D.Dynamic;
                    Jump();
                }
                break;
            case State.Playing:
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
                break;
            case State.Dead:
                break;

        }

    }

    public static void Jump()
    {
        baseRB.GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpFloat;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        baseRB.bodyType = RigidbodyType2D.Static;
        state = State.Dead;
        if (onDied != null) onDied(this, EventArgs.Empty); 
    }


} 
