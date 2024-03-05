using System.Collections;
using UnityEngine;

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
        if (_coroutineActive != null)
        {
            StopCoroutine(_coroutineActive);
        }

        _coroutineActive = StartCoroutine(FadeInVolumeAlarm());
    }

    public void DisableAlarm()
    {
        if (_coroutineActive != null)
        {
            StopCoroutine(_coroutineActive);
        }

        _coroutineActive = StartCoroutine(FadeOutVolumeAlarm());
    }

    private IEnumerator FadeInVolumeAlarm()
    {
        PlayAudioAlarmSystem();

        _audioAlarmSystem.volume = _minVolumeAlarmSystem;

        while (_audioAlarmSystem.volume < _maxVolumeAlarmSystem)
        {
            ChangeVolumeAlarm(_audioAlarmSystem.volume, _maxVolumeAlarmSystem, _speed);

            yield return null;
        }
    }

    private IEnumerator FadeOutVolumeAlarm()
    {
        PlayAudioAlarmSystem();

        while (_audioAlarmSystem.volume > _minVolumeAlarmSystem)
        {
            ChangeVolumeAlarm(_audioAlarmSystem.volume, _minVolumeAlarmSystem, _speed);

            yield return null;
        }

        _audioAlarmSystem.Stop();
    }

    private void ChangeVolumeAlarm(float initialVolume, float finalVolume, float speed)
    {
        _audioAlarmSystem.volume = Mathf.MoveTowards(initialVolume, finalVolume, speed * Time.deltaTime);
    }

    private void PlayAudioAlarmSystem()
    {
        _audioAlarmSystem.Play();
    }
}
