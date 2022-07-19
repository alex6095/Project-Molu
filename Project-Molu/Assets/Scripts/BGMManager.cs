using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance = null;
    AudioSource audioSource;
    public AudioClip loginBGM;
    public AudioClip mainBGM;
    public AudioClip battleBGM;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else
        {
            Destroy(gameObject);
        }

        // 이렇게 하면 다음 scene으로 넘어가도 오브젝트가 사라지지 않습니다.
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayLoginBGM()
    {
        audioSource.Stop();
        audioSource.clip = loginBGM;
        audioSource.loop = true;
        audioSource.time = 0;
        audioSource.Play();
    }

    public void PlayMainBGM()
    {
        audioSource.Stop();
        audioSource.clip = mainBGM;
        audioSource.loop = true;
        audioSource.time = 0;
        audioSource.Play();
    }

    public void PlayBattleBGM()
    {
        audioSource.Stop();
        audioSource.clip = battleBGM;
        audioSource.loop = true;
        audioSource.time = 0;
        audioSource.Play();
    }
}
