using System;
using UnityEngine;

public class WaitToStart : MonoBehaviour
{
    private void Start()
    {
        Bird.getInstance().onStart += Bird_onStart;
    }

    private void Bird_onStart(object sender, EventArgs e)
    {
        gameObject.SetActive(false);
    }
}
