using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������
/// </summary>
public class VocalProp : MonoBehaviour
{

    //����ģʽ
    public bool isLoop = false;
    //���㼶
    public LayerMask detectMask;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("���봥����");
        if (((1 << collision.gameObject.layer) & detectMask) != 0)
        {
            Debug.Log("�����㼶");
            if (!_audioSource.isPlaying)
            {
                _audioSource.loop = isLoop;
                _audioSource.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("�뿪������");
        _audioSource.Stop();
    }
}
