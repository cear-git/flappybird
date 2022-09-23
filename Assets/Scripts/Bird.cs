using System;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private static Rigidbody2D baseRB;
    private const float jumpFloat = 90f;
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
                    state = State.Playing;
                }
                break;
            case State.Playing:
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }

                transform.eulerAngles = new Vector3(0, 0, baseRB.velocity.y * .25f);
                break;
            case State.Dead:
                break;

        }

    }

    public static void Jump()
    {
        baseRB.GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpFloat;
        SoundManager.Play(SoundManager.Sound.Jump);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        baseRB.bodyType = RigidbodyType2D.Static;
        state = State.Dead;
        SoundManager.Play(SoundManager.Sound.Lose);
        if (onDied != null) onDied(this, EventArgs.Empty); 
    }


} 
