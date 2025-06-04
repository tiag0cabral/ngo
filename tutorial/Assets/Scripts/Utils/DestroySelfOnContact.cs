using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfOnContact : MonoBehaviour
{
    private void onTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }
}
