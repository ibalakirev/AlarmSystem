using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Alarmor : MonoBehaviour
{
    private Sensor _sensor;
    private AudioSource _audioAlarmSystem;
    private Coroutine _coroutineActive;
    private float _minVolumeAlarmSystem = 0f;
    private float _maxVolumeAlarmSystem = 1f;
    private float _speed = 0.1f;

    private void Awake()
    {
        _audioAlarmSystem = GetComponent<AudioSource>();
        _sensor = FindObjectOfType<Sensor>();
    }

    private void OnEnable()
    {
        _sensor.DetectionCriminalEnable += EnableAlarm;
        _sensor.DetectionCriminalDisable += DisableAlarm;
    }

    private void OnDisble()
    {
        _sensor.DetectionCriminalEnable -= EnableAlarm;
        _sensor.DetectionCriminalDisable -= DisableAlarm;
    }
    public void EnableAlarm()
    {
        ManageStateAlarm(ChangeVolumeAlarm(_maxVolumeAlarmSystem));
    }

    public void DisableAlarm()
    {
        ManageStateAlarm(ChangeVolumeAlarm(_minVolumeAlarmSystem));
    }

    private void ManageStateAlarm(IEnumerator changingVolumeAlarm)
    {
        if (_coroutineActive != null)
        {
            StopCoroutine(_coroutineActive);
        }

        _coroutineActive = StartCoroutine(changingVolumeAlarm);
    }

    private IEnumerator ChangeVolumeAlarm(float finalVolumeAlarm)
    {
        _audioAlarmSystem.Play();

        while (_audioAlarmSystem.volume != finalVolumeAlarm)
        {
            _audioAlarmSystem.volume = Mathf.MoveTowards(_audioAlarmSystem.volume, finalVolumeAlarm, _speed * Time.deltaTime);

            yield return null;
        }

        if (_audioAlarmSystem.volume == _minVolumeAlarmSystem)
        {
            _audioAlarmSystem.Stop();
        }
    }
}
