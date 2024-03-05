using UnityEngine;
using UnityEngine.Events;

public class Sensor : MonoBehaviour
{
    private const string Criminal = nameof(Criminal);

    [SerializeField] private UnityEvent _detectionCriminalEnable;
    [SerializeField] private UnityEvent _detectionCriminalDisable;
    [SerializeField] private ControlerCriminal _criminal;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (VerifyComponentCollider(other))
        {
            _detectionCriminalEnable.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (VerifyComponentCollider(other))
        {
            _detectionCriminalDisable.Invoke();
        }
    }

    private bool VerifyComponentCollider(Collider2D other)
    {
        return other.GetComponent<ControlerCriminal>();
    }
}
