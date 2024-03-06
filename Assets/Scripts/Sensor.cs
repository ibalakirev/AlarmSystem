using System;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public event Action DetectionCriminalEnable;
    public event Action DetectionCriminalDisable;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (VerifyComponentCollider(other))
        {
            DetectionCriminalEnable.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (VerifyComponentCollider(other))
        {
            DetectionCriminalDisable.Invoke();
        }
    }

    private bool VerifyComponentCollider(Collider2D other)
    {
        return other.GetComponent<ControlerCriminal>();
    }
}
