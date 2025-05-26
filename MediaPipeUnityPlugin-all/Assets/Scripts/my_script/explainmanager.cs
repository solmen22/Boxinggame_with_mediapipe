using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class explainmanager : MonoBehaviour
{
    // Start is called before the first frame update

    private int nowpage = 1;
    public GameObject Scene1;
    public GameObject Scene2;
    public GameObject Scene3;
    public GameObject Scene4;
    public GameObject Scene5;
    public GameObject Scene6;
 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void backgame()
    {
       
        
    }

    public void next()
    {
        switch (nowpage)
        {
            case 1:
                Scene1.SetActive(false);
                break;
            case 2:
                Scene2.SetActive(false);
                break;
            case 3:
                Scene3.SetActive(false);
                break;
            case 4:
                Scene4.SetActive(false);
                break;
            case 5:
                Scene5.SetActive(false);
                break;

        }

        nowpage++;
        switch(nowpage)
        {
            case 2:
                Scene2.SetActive(true);
                break;
            case 3:
                Scene3.SetActive(true);
                break;
            case 4:
                Scene4.SetActive(true);
                break;
            case 5:
                Scene5.SetActive(true);
                break;
            case 6:
                Scene6.SetActive(true);
                break;
        }
    }

    public void before()
    {
        switch (nowpage)
        {
            case 2:
                Scene2.SetActive(false);
                break;
            case 3:
                Scene3.SetActive(false);
                break;
            case 4:
                Scene4.SetActive(false);
                break;
            case 5:
                Scene5.SetActive(false);
                break;
            case 6:
                Scene6.SetActive(false);
                break;
        }

        nowpage--;
        switch (nowpage)
        {
            case 1:
                Scene1.SetActive(true);
                break;
            case 2:
                Scene2.SetActive(true);
                break;
            case 3:
                Scene3.SetActive(true);
                break;
            case 4:
                Scene4.SetActive(true);
                break;
            case 5:
                Scene5.SetActive(true);
                break;
        }
    }

}
