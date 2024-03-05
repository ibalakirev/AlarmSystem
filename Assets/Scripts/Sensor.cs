using UnityEngine;
using UnityEngine.Events;

public class Sensor : MonoBehaviour
{
    private const string Criminal = nameof(Criminal);

    [SerializeField] private UnityEvent _detectionCriminalEnable;
    [SerializeField] private UnityEvent _detectionCriminalDisable;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Criminal))
        {
            _detectionCriminalEnable.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Criminal))
        {
            _detectionCriminalDisable.Invoke();
        }
    }
}
