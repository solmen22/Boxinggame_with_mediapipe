using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using Microsoft.Azure.Kinect.BodyTracking;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using Microsoft.Azure.Kinect.Sensor;
//using UnityEditor.Animations;





public class Gamecontroller : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> Levels = new List<GameObject>();
    public GameObject Canvas;
    public GameObject WinCanvas;
    public GameObject LoseCanvas;
    public GameObject DrawCanvas;
  
    public GameObject timeup;
    public GameObject nextlevel;
    public hpscript enemyhp;
    public enemyhpscripthpscript playerhp;
    public GameTimer gameTimer;
    public Animator player;
    public Animator enemy;
    public Transform enemyt;
    public AudioClip gong;
    public AudioClip Winapplause;
    public AudioClip countdown;
    public AudioClip regret;
    public AudioClip falldown;
    public AudioClip bgm;
    public AudioSource AudioSource;
    public AudioSource Countdown;
    public AudioSource Bgm;
    
    

    
    public bool Stop => stoping;
    private int MAXLEVEL = 4;
    private bool stoping = false;
    private bool win = false;
    private bool lose = false;
    private bool draw = false;
    private int count = 0;
    private bool countd = false;
    Vector3 defaultposition = new Vector3(4.96999979f, 0f, -2.46000004f);
    
   
    private void Start()
    {
        
        count = 0;
        Bgm.Play();
    }

  void OnEnable()
  {
    MAXLEVEL = 4;
    stoping = false;
    win = false;
    lose = false;
    draw = false;
    count = 0;
    countd = false;
    Vector3 defaultposition = new Vector3(4.96999979f, 0f, -2.46000004f);
    Start();
  }

  // Update is called once per frame
  void Update()
    {
        if(count >= 2)
        {
            return;
        }
        if(gameTimer.Timer <= 5 && !countd)
        {
            Countdown.PlayOneShot(countdown);
            countd = true;
        }
        if(gameTimer.Timer > 0)
        {
            if (enemyhp.currentRate <= 0f && playerhp.currentRate >0f)
            {
                stoping = true;
                win = true;
                
                player.Play("pwin");
                
                if (count == 0)
                {
                    
                    Countdown.Stop();
                    AudioSource.PlayOneShot(Winapplause);
                    enemy.Play("knocked");
                    count++;
                }
                
               

            }else if(enemyhp.currentRate > 0f && playerhp.currentRate <= 0f)
            {
                lose = true;
                stoping=true;
              
                player.Play("pknocked");
                if (count == 0)
                {
          Countdown.Stop();
                    enemy.Play("win");
                    count++;
                }
                else
                {
                    enemyt.transform.rotation = Quaternion.Slerp(enemyt.transform.rotation, Quaternion.Euler(0f, 17.253f, 0f), Time.deltaTime * 5f);
                    enemyt.transform.position = Vector3.MoveTowards(enemyt.transform.position, defaultposition, Time.deltaTime * 5f);
                }

            }
            else if (enemyhp.currentRate == 0f && playerhp.currentRate == 0f)
            {
                draw = true;
                stoping = true;
               
                player.Play("pknocked");
                if (count == 0)
                {
                    Countdown.Stop();
                    StartCoroutine(Regret());
                    enemy.Play("knocked");
                    count++;
                }
                


            }
        }else
        {
            if (enemyhp.currentRate <= 0f && playerhp.currentRate > 0f)
            {
                win = true;
                stoping = true;
               
                player.Play("pwin");
                if (count == 0)
                {
                    
                    Countdown.Stop();
                    AudioSource.PlayOneShot(Winapplause);
                    enemy.Play("knocked");
                    count++;
                }
                

            }
            else if (enemyhp.currentRate > 0f && playerhp.currentRate <= 0f)
            {
                lose = true;
                stoping = true;

                player.Play("pknocked");
                if (count == 0)
                {
                    Countdown.Stop();
                    enemy.Play("win");
                    count++;
                }
                else
                {
                    enemyt.transform.rotation = Quaternion.Slerp(enemyt.transform.rotation, Quaternion.Euler(0f, 17.253f, 0f), Time.deltaTime * 5f);
                    enemyt.transform.position = Vector3.MoveTowards(enemyt.transform.position, defaultposition, Time.deltaTime * 5f);
                }



            }
            else if (enemyhp.currentRate == 0f && playerhp.currentRate == 0f)
            {
                
                draw = true;
                stoping = true;
             
                player.Play("pknocked");
                if (count == 0)
                {
                    Countdown.Stop();
                    StartCoroutine(Regret());
                    enemy.Play("knocked");
                    count++;
                }
                


            }
            else
            {
                if(count == 0)
                {
                    StartCoroutine(Timeup());
                }
                
                lose = true;
                stoping = true;
                
                player.Play("pdefeat");
                if (count == 0)
                {
                    Countdown.Stop();
                    StartCoroutine(Regret());
                    enemy.Play("win");
                    count++;
                }
                else
                {
                    enemyt.transform.rotation = Quaternion.Slerp(enemyt.transform.rotation, Quaternion.Euler(0f, 17.253f, 0f), Time.deltaTime * 5f);
                    enemyt.transform.position = Vector3.MoveTowards(enemyt.transform.position, defaultposition, Time.deltaTime * 5f);
                }


            }

        }

        
    }

    IEnumerator Regret()
    {
        yield return new WaitForSeconds(1.0f);
        AudioSource.PlayOneShot(regret);
    }

    IEnumerator Timeup()
    {
        timeup.SetActive(true);
        AudioSource.PlayOneShot(gong);
        yield return new WaitForSeconds(1.0f);
        timeup.SetActive(false);

    }

    public void NextLevel_Restart()
    {
        
        int level = PlayerPrefs.GetInt("level", 4);
        
        if (level < MAXLEVEL)
        {
            level++;
            PlayerPrefs.SetInt("level", level);
            PlayerPrefs.Save();
            
        }
        foreach(GameObject obj in Levels)
    {
      obj.SetActive(false);
    }
        nextlevel.SetActive(false);
        
    }
       
    public void Restart()
    {
        WinCanvas.SetActive(false);
        LoseCanvas.SetActive(false);
        DrawCanvas.SetActive(false);

    foreach (GameObject obj in Levels)
    {
      obj.SetActive(false);
    }
  }

    public void finish()
    {

       WinCanvas.SetActive(false);
       LoseCanvas.SetActive(false);
       DrawCanvas.SetActive(false);
    nextlevel.SetActive(false);
    foreach (GameObject obj in Levels)
    {
      obj.SetActive(false);
    }
  }

    public void judge()
    {
        
        if (draw)
        {
            Time.timeScale = 0f;
            DrawCanvas.SetActive(true);
            Canvas.SetActive(false);
        }else if (lose)
        {
            Time.timeScale = 0f;
            LoseCanvas.SetActive(true);
            Canvas.SetActive(false);

        }else if (win)
        {
            Time.timeScale = 0f;
            int level = PlayerPrefs.GetInt("level", 4);
            if (level < MAXLEVEL)
            {
                nextlevel.SetActive(true);
            }
            else
            {
                WinCanvas.SetActive(true);
            }

                
            Canvas.SetActive(false);
        }

    }

    public void down()
    {
        AudioSource.PlayOneShot(falldown);
    }





}
