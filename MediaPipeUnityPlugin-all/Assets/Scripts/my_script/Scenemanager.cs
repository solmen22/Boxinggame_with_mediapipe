using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenemanager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Boxing_gameObjects = new List<GameObject>();
    public List<GameObject> Start_gameObjects = new List<GameObject>();
    public List<GameObject> Guide_gameObjects = new List<GameObject>();
    public AudioSource audioSource;
  void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reload_boxing_scene()
  {
    foreach (GameObject obj in Boxing_gameObjects)
    {
      obj.SetActive(false);
    }

    foreach (GameObject obj in Boxing_gameObjects)
    {
      obj.SetActive(true);
    }
  }

    public void Change_start_to_boxing_scene()
  {
    audioSource.Stop();
    foreach (GameObject obj in Boxing_gameObjects)
    {
      obj.SetActive(true);
    }
    
    foreach (GameObject obj in Start_gameObjects)
    {
      obj.SetActive(false);
    }
  }
   public void Change_boxing_scene_to_start()
  {
    audioSource.Play();
    foreach (GameObject obj in Boxing_gameObjects)
    {
      obj.SetActive(false);
    }

    foreach (GameObject obj in Start_gameObjects)
    {
      obj.SetActive(true);
    }
  }
  public void Change_start_to_guide()
  {
    foreach (GameObject obj in Guide_gameObjects)
    {
      obj.SetActive(true);
    }

    foreach (GameObject obj in Start_gameObjects)
    {
      obj.SetActive(false);
    }
  }
  public void Change_guide_to_start()
  {
    foreach (GameObject obj in Guide_gameObjects)
    {
      obj.SetActive(false);
    }

    foreach (GameObject obj in Start_gameObjects)
    {
      obj.SetActive(true);
    }
  }

}
