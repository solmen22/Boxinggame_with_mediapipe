using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Button> button;
    public AudioClip button_sound;
    AudioSource AudioSource;

  private void OnEnable()
  {
    Start();
  }
  void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        foreach (var button in button)
        {

            button.onClick.AddListener(PlaySound);
        }

    }


    void PlaySound()
    {
        if(button_sound != null)
        {
            AudioSource.PlayOneShot(button_sound);
        }
    }
}
