using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Gamemanager : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject levelCanvas;
   

   
   
    public void SelectlevelButton()
    {
     
        Canvas.SetActive(false);
        levelCanvas.SetActive(true);

    }

    public void Easy()
    {
        PlayerPrefs.SetInt("level", 1);
    Canvas.SetActive(true);
    levelCanvas.SetActive(false);

  }

    public void Normal()
    {
        PlayerPrefs.SetInt("level", 2);
    Canvas.SetActive(true);
    levelCanvas.SetActive(false);

  }

    public void Hard()
    {
        PlayerPrefs.SetInt("level", 3);
    Canvas.SetActive(true);
    levelCanvas.SetActive(false);

  }

    public void Hell()
    {
        PlayerPrefs.SetInt("level", 4);
    Canvas.SetActive(true);
    levelCanvas.SetActive(false);

  }

    public void Guide()
    {
        
        
    }

    public void Back()
    {
        Canvas.SetActive(true);
        levelCanvas.SetActive(false);
    }
}
