using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Alarmor : MonoBehaviour
{
    private AudioSource _audioAlarmSystem;
    private Coroutine _coroutineActive;
    private float _minVolumeAlarmSystem = 0f;
    private float _maxVolumeAlarmSystem = 1f;
    private float _speed = 0.1f;

    private void Awake()
    {
        _audioAlarmSystem = GetComponent<AudioSource>();
    }

    public void EnableAlarm()
    {
        ManageStateAlarm(FadeInVolumeAlarm());
    }

    public void DisableAlarm()
    {
        ManageStateAlarm(FadeOutVolumeAlarm());
    }

    private void ManageStateAlarm(IEnumerator changingVolumeAlarm)
    {
        if (_coroutineActive != null)
        {
            StopCoroutine(_coroutineActive);
        }

        _coroutineActive = StartCoroutine(changingVolumeAlarm);
    }

    private IEnumerator FadeInVolumeAlarm()
    {
        _audioAlarmSystem.Play();


        _audioAlarmSystem.volume = _minVolumeAlarmSystem;

        while (GetConditionChangingVolumeAlarm(_maxVolumeAlarmSystem, _audioAlarmSystem.volume))
        {
            ChangeVolumeAlarm(_audioAlarmSystem.volume, _maxVolumeAlarmSystem, _speed);

            yield return null;
        }
    }

    private IEnumerator FadeOutVolumeAlarm()
    {
        while (GetConditionChangingVolumeAlarm(_audioAlarmSystem.volume, _minVolumeAlarmSystem))
        {
            ChangeVolumeAlarm(_audioAlarmSystem.volume, _minVolumeAlarmSystem, _speed);

            yield return null;
        }

        _audioAlarmSystem.Stop();
    }

    private bool GetConditionChangingVolumeAlarm(float InitialVolumeAlarm, float finalVolumeAlarm)
    {
        return InitialVolumeAlarm > finalVolumeAlarm;
    }

    private void ChangeVolumeAlarm(float initialVolume, float finalVolume, float speed)
    {
        _audioAlarmSystem.volume = Mathf.MoveTowards(initialVolume, finalVolume, speed * Time.deltaTime);
    }
}
