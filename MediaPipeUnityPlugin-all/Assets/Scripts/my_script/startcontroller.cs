using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class startcontroller : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> objectsToActivate;
    public GameObject Ready;
    public GameObject Fight;
    public GameObject player;
    public GameObject enemy;
    public AudioClip fight_sound;
    public AudioClip ready_sound;
    AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(false);
        }

        player.GetComponent<bodymode>().enabled = false;
        enemy.GetComponent<enemybody>().enabled = false;
    }
    void Start()
    {
        

        // 開始処理をコルーチンで実行
        StartCoroutine(StartGameRoutine());
    }
  void OnEnable()
  {
    Awake();
    Start();
  }

  IEnumerator StartGameRoutine()
    {
       
        Ready.SetActive(true);
        audioSource.PlayOneShot(ready_sound);
        yield return new WaitForSeconds(1.5f);
        Ready.SetActive(false);

        // 「Fight!」表示
        Fight.SetActive(true);
        audioSource.PlayOneShot(fight_sound);
        yield return new WaitForSeconds(1.0f);
        Fight.SetActive(false);

        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
        player.GetComponent<bodymode>().enabled = true;
        enemy.GetComponent<enemybody>().enabled = true;

        Time.timeScale = 1.0f;
    }
}
