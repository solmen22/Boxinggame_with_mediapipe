using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using UnityEditor.PackageManager;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine.UI;
using DG.Tweening;
using Mediapipe.Unity;
using System;

public class bodymode : MonoBehaviour
{
  [SerializeField] private PointListAnnotation pointListAnnotation;  // Inspectorから設定
                                                                     //public TextMeshProUGUI Mode;
  
    public Transform player;
   
    public GameObject explosion_right;
    public GameObject Critical_right;
    public GameObject Guard;
    public GameObject Hit;
    public GameObject Hit_effect;
    public GameObject StrongHit;
    public GameObject StrongHit_effect;
    public GameObject Miss;
    public Transform spine2;
    public enemybody enemy;
    public Transform lefthand;
    public Transform righthand;
    public GameObject easy;
    public GameObject normal;
    public GameObject hard;
    public GameObject hell;
    public AudioClip punch;
    public AudioClip Hit_sound;
    public AudioClip StrongHit_sound;
    public AudioClip Critical_sound;
    public AudioClip Guard_sound;


    AudioSource audiosource;
    

   
    private Animator animator;
  private double distanse_right;
  private double distanse_left;
  private double body_angle;
    
    private bool rightpunch = false;
    private bool leftpunch = false;
  private bool counterrightpunch = false;
  private bool counterleftpunch = false;
    private bool blocking = false;
    private bool rightpunching = false;
    private bool leftpunching = false;
    private bool counterrightpunching = false;
    private bool counterleftpunching = false;
    private bool afterrightpunch = false;
    private bool afterleftpunch = false;
    private bool aftercounterleftpunch = false;
    private bool aftercounterrightpunch = false;

    public bool IsRightPunch => rightpunching;
    public bool IsLeftPunch => leftpunching;
    public bool IsCounterRight => counterrightpunching;

    public bool IsCounterLeft => counterleftpunching;
    public bool AfRightPunch => afterrightpunch;
    public bool AfLeftPunch => afterleftpunch;
    public bool AfCouterRightPunch => aftercounterrightpunch;
    public bool AFCounterLeftPunch => aftercounterleftpunch;
    public bool IsBlocking => blocking;
    public Gamecontroller Gamecontroller;
    
    private bool fina = true;
    Vector3 defaultposition = new Vector3(4.96376514f, 0.0190830734f, 1.08287179f);

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
        Time.timeScale = 1.0f;
        int level = PlayerPrefs.GetInt("level",4);
        switch (level)
        {
            case 1:
                easy.SetActive(true);
                break;
            case 2:
                normal.SetActive(true);
                break;
            case 3:
                hard.SetActive(true);
                break;
            case 4:
                hell.SetActive(true);  
                break;
        }
    }

    void Start()
    {

        animator = GetComponent<Animator>();
        //Mode.SetText("blockingMode");
        rightpunch = false;
        leftpunch = false;
        counterleftpunch = false;
        counterrightpunch = false;
        blocking = true;
        
    }

  void OnEnable()
  {
    rightpunch = false;
    leftpunch = false;
    counterrightpunch = false;
    counterleftpunch = false;
    blocking = false;
    rightpunching = false;
    leftpunching = false;
    counterrightpunching = false;
    counterleftpunching = false;
    afterrightpunch = false;
    afterleftpunch = false;
    aftercounterleftpunch = false;
    aftercounterrightpunch = false;
    fina = true;
    player.transform.position = defaultposition;
    player.transform.rotation = Quaternion.Euler(0f, -160f, 0f);
    Awake();
    Start();
  }

  // Update is called once per frame
  void Update()
    {
        
        
        if (IsPlayingAnimation())
        {
            if(PlayingAnimationNow(animator, "counterleftpunch"))
            {
                
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0f, 225.153f, 0f), Time.deltaTime * 5f);

            }else if(PlayingAnimationNow(animator, "counterrightpunch"))
            {
               
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0f, -220f, 0f), Time.deltaTime * 5f);

            } else if(PlayingAnimationNow(animator, "leftpunch"))
            {
               
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0f, 162.747f, 0f), Time.deltaTime * 5f);
            }
            
            return;
        }
        else
        {

            player.transform.position = Vector3.MoveTowards(player.transform.position, defaultposition, Time.deltaTime * 5f);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0f, -160f, 0f), Time.deltaTime * 5f);
            blocking = true;
            afterrightpunch = false;
            afterleftpunch = false;
            aftercounterleftpunch = false;
            aftercounterrightpunch = false;
        }

           

        distanse_right = Vector3.Distance(pointListAnnotation.aChildren[0].transform.position,pointListAnnotation.aChildren[16].transform.position);
        distanse_left = Vector3.Distance(pointListAnnotation.aChildren[0].transform.position, pointListAnnotation.aChildren[15].transform.position);
        Vector2 dt = pointListAnnotation.aChildren[11].transform.position - pointListAnnotation.aChildren[23].transform.position;
        body_angle = Mathf.Atan2(dt.y, dt.x);
        //Debug.Log("rightpunch:" + Vector3.Distance(pointListAnnotation.aChildren[0].transform.position, pointListAnnotation.aChildren[16].transform.position));
        //Debug.Log("leftpunch:" + Vector3.Distance(pointListAnnotation.aChildren[0].transform.position, pointListAnnotation.aChildren[15].transform.position));


    if (fina == true)
        {
            if (body_angle < 1.55)
            {
                rightpunch = false;
                leftpunch = false;
                counterrightpunch = true;
                counterleftpunch = false;
                blocking = true;
                fina = false;
                counterrightpunching = true;
                StartCoroutine(PunchPlayDelayedSound(0.4f));
                animator.SetTrigger("counterrightpunch");
                //Mode.SetText("counterrightpunchbefore");
            }
            else if (body_angle > 1.75)
            {

                rightpunch = false;
                leftpunch = false;
                counterrightpunch = false;
                counterleftpunch = true;
                blocking = true;
                fina = false;
                counterleftpunching = true;
                StartCoroutine(PunchPlayDelayedSound(0.4f));
                animator.SetTrigger("counterleftpunch");
                //Mode.SetText("counterleftpunchbefore");
            }
            //else if (Mathf.Abs(right_elbow_transform.localEulerAngles.z - elbow_center_angle) <= er)
            else if (distanse_right > 70 && !(distanse_left > 70))
            {


                rightpunch = true;
                leftpunch = false;
                counterleftpunch = false;
                counterrightpunch = false;
                blocking = true;
                fina = false;
                rightpunching = true;
                StartCoroutine(PunchPlayDelayedSound(0.2f));
                animator.SetTrigger("rightpunch");
                //Mode.SetText("rightpunchbeforeMode");



            }
            //else if (Mathf.Abs(left_elbow_angle + elbow_center_angle) <= er)
            else if (!(distanse_right > 70) && (distanse_left > 70))
      {


                rightpunch = false;
                leftpunch = true;
                counterleftpunch = false;
                counterrightpunch = false;
                blocking = true;
                fina = false;
                leftpunching = true;
                StartCoroutine(PunchPlayDelayedSound(0.2f));
                animator.SetTrigger("leftpunch");
                //Mode.SetText("leftpunchbeforeMode");



            }
            else
            {
                //Mode.SetText("blockingMode");
                rightpunch = false;
                leftpunch = false;
                counterleftpunch = false;
                counterrightpunch = false;
                blocking = true;

            }
        }
        





    }


    IEnumerator PunchPlayDelayedSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        audiosource.PlayOneShot(punch);
    }
    bool IsPlayingAnimation()
    {
        if (animator == null) return false;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime < 1f && stateInfo.loop == false;
    }

    bool PlayingAnimationNow(Animator animator, string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName);
    }

    public void counterl()
    {

        if (enemy.IsLeftPunch || enemy.AfLeftPunch)
        {
            Vector3 exvec = new Vector3(-0.0326f, 0.1372f, 0.0095f);
            Vector3 crivec = new Vector3(-0.670000017f, -0.101999998f, -0.0149999997f);

            Vector3 explosionPosition = lefthand.TransformPoint(exvec);
            GameObject explosion = Instantiate(explosion_right, explosionPosition, Quaternion.identity);
            explosion.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            GameObject Critical = Instantiate(Critical_right, spine2);


            Critical.transform.localPosition = crivec;
            Critical.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            ParticleSystem explosionParticle = explosion.GetComponent<ParticleSystem>();
            if (explosionParticle != null)
            {
                explosionParticle.Play();
            }

            ParticleSystem criticalParticle = Critical.GetComponent<ParticleSystem>();
            if (criticalParticle != null)
            {
                criticalParticle.Play();
            }
            audiosource.PlayOneShot(Critical_sound);
            Destroy(explosion, 3f);
            Destroy(Critical, 3f);



        } else if (enemy.IsRightPunch || enemy.AfRightPunch || enemy.IsCounterLeft || enemy.IsCounterRight || enemy.AfCouterRightPunch || enemy.AFCounterLeftPunch)
        {

            Vector3 exvec = new Vector3(-0.0326f, 0.1372f, 0.0095f);
            Vector3 crivec = new Vector3(-0.670000017f, -0.101999998f, -0.0149999997f);

            Vector3 explosionPosition = lefthand.TransformPoint(exvec);
            GameObject Hit_exp = Instantiate(Hit_effect, explosionPosition, Quaternion.identity);
            Hit_exp.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            GameObject Hit_scr = Instantiate(Hit, spine2);


            Hit_scr.transform.localPosition = crivec;
            Hit_scr.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            ParticleSystem explosionParticle = Hit_exp.GetComponent<ParticleSystem>();
            if (explosionParticle != null)
            {
                explosionParticle.Play();
            }

            ParticleSystem criticalParticle = Hit_scr.GetComponent<ParticleSystem>();
            if (criticalParticle != null)
            {
                criticalParticle.Play();
            }
            audiosource.PlayOneShot(Hit_sound);
            
            Destroy(Hit_scr, 3f);
            Destroy(Hit_exp, 3f);


        }
        else if (enemy.IsBlocking)
        {
            Vector3 guarvec = new Vector3(-0.670000017f, -0.101999998f, -0.0149999997f);
            GameObject Guardins = Instantiate(Guard, spine2);
            Guardins.transform.localPosition = guarvec;
            Guardins.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            ParticleSystem criticalParticle = Guardins.GetComponent<ParticleSystem>();
            if (criticalParticle != null)
            {
                criticalParticle.Play();
            }
            audiosource.PlayOneShot(Guard_sound);
           
            Destroy(Guardins, 3f);

        }

    }

    public void counterr()
    {

        if (enemy.IsRightPunch || enemy.AfRightPunch)
        {
            Vector3 exvec = new Vector3(0.254000008f, -0.0140000004f, 0.00300000003f);
            Vector3 crivec = new Vector3(-0.670000017f, -0.101999998f, -0.0149999997f);

            Vector3 explosionPosition = righthand.TransformPoint(exvec);
            GameObject explosion = Instantiate(explosion_right, explosionPosition, Quaternion.identity);
            explosion.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            GameObject Critical = Instantiate(Critical_right, spine2);


            Critical.transform.localPosition = crivec;
            Critical.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            ParticleSystem explosionParticle = explosion.GetComponent<ParticleSystem>();
            if (explosionParticle != null)
            {
                explosionParticle.Play();
            }

            ParticleSystem criticalParticle = Critical.GetComponent<ParticleSystem>();
            if (criticalParticle != null)
            {
                criticalParticle.Play();
            }
            audiosource.PlayOneShot(Critical_sound);
            
            Destroy(explosion, 3f);
            Destroy(Critical, 3f);



        }
        else if (enemy.IsLeftPunch || enemy.AfLeftPunch || enemy.IsCounterLeft || enemy.IsCounterRight || enemy.AfCouterRightPunch || enemy.AFCounterLeftPunch)
        {

            Vector3 exvec = new Vector3(0.254000008f, -0.0140000004f, 0.00300000003f);
            Vector3 crivec = new Vector3(-0.670000017f, -0.101999998f, -0.0149999997f);

            Vector3 explosionPosition = righthand.TransformPoint(exvec);
            GameObject Hit_exp = Instantiate(Hit_effect, explosionPosition, Quaternion.identity);
            Hit_exp.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            GameObject Hit_scr = Instantiate(Hit, spine2);


            Hit_scr.transform.localPosition = crivec;
            Hit_scr.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            ParticleSystem explosionParticle = Hit_exp.GetComponent<ParticleSystem>();
            if (explosionParticle != null)
            {
                explosionParticle.Play();
            }

            ParticleSystem criticalParticle = Hit_scr.GetComponent<ParticleSystem>();
            if (criticalParticle != null)
            {
                criticalParticle.Play();
            }

            audiosource.PlayOneShot(Hit_sound);
           
            Destroy(Hit_scr, 3f);
            Destroy(Hit_exp, 3f);


        }
        else if (enemy.IsBlocking)
        {
            Vector3 guarvec = new Vector3(-0.670000017f, -0.101999998f, -0.0149999997f);
            GameObject Guardins = Instantiate(Guard, spine2);
            Guardins.transform.localPosition = guarvec;
            Guardins.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            ParticleSystem criticalParticle = Guardins.GetComponent<ParticleSystem>();
            if (criticalParticle != null)
            {
                criticalParticle.Play();
            }

            audiosource.PlayOneShot(Guard_sound);
            
            Destroy(Guardins, 3f);

        }


    }

    public void punchl()
    {
        if (enemy.IsLeftPunch || enemy.AfLeftPunch || enemy.IsRightPunch || enemy.AfRightPunch || enemy.AFCounterLeftPunch)
        {

            Vector3 exvec = new Vector3(-0.0326f, 0.1372f, 0.0095f);
            Vector3 crivec = new Vector3(-0.670000017f, -0.101999998f, -0.0149999997f);

            Vector3 explosionPosition = lefthand.TransformPoint(exvec);
            GameObject Hit_exp = Instantiate(Hit_effect, explosionPosition, Quaternion.identity);
            Hit_exp.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            GameObject Hit_scr = Instantiate(Hit, spine2);


            Hit_scr.transform.localPosition = crivec;
            Hit_scr.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            ParticleSystem explosionParticle = Hit_exp.GetComponent<ParticleSystem>();
            if (explosionParticle != null)
            {
                explosionParticle.Play();
            }

            ParticleSystem criticalParticle = Hit_scr.GetComponent<ParticleSystem>();
            if (criticalParticle != null)
            {
                criticalParticle.Play();
            }

            audiosource.PlayOneShot(Hit_sound);
            
            Destroy(Hit_scr, 3f);
            Destroy(Hit_exp, 3f);



        }else if(enemy.IsCounterRight || enemy.AfCouterRightPunch)
        {

            Vector3 exvec = new Vector3(-0.0326f, 0.1372f, 0.0095f);
            Vector3 crivec = new Vector3(-0.670000017f, -0.101999998f, -0.0149999997f);

            Vector3 explosionPosition = lefthand.TransformPoint(exvec);
            GameObject StrongHit_exp = Instantiate(StrongHit_effect, explosionPosition, Quaternion.identity);
            StrongHit_exp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            GameObject StrongHit_scr = Instantiate(StrongHit, spine2);


            StrongHit_scr.transform.localPosition = crivec;
            StrongHit_scr.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            ParticleSystem explosionParticle = StrongHit_exp.GetComponent<ParticleSystem>();
            if (explosionParticle != null)
            {
                explosionParticle.Play();
            }

            ParticleSystem criticalParticle = StrongHit_scr.GetComponent<ParticleSystem>();
            if (criticalParticle != null)
            {
                criticalParticle.Play();
            }

            audiosource.PlayOneShot(StrongHit_sound);
            
            Destroy(StrongHit_exp, 3f);
            Destroy(StrongHit_scr, 3f);

        }else if (enemy.IsCounterLeft)
        {
            Vector3 missvec = new Vector3(-0.670000017f, -0.101999998f, -0.0149999997f);
            GameObject Miss_scr = Instantiate(Miss, spine2);
            Miss_scr.transform.localPosition = missvec;
            Miss_scr.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            ParticleSystem criticalParticle = Miss_scr.GetComponent<ParticleSystem>();
            if (criticalParticle != null)
            {
                criticalParticle.Play();
            }
            Destroy(Miss_scr, 3f);
        }
        else if (enemy.IsBlocking)
        {
            Vector3 guarvec = new Vector3(-0.670000017f, -0.101999998f, -0.0149999997f);
            GameObject Guardins = Instantiate(Guard, spine2);
            Guardins.transform.localPosition = guarvec;
            Guardins.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            ParticleSystem criticalParticle = Guardins.GetComponent<ParticleSystem>();
            if (criticalParticle != null)
            {
                criticalParticle.Play();
            }

            audiosource.PlayOneShot(Guard_sound);
            
            Destroy(Guardins, 3f);

        }

    }

    public void punchr()
    {

        if (enemy.IsLeftPunch || enemy.AfLeftPunch || enemy.IsRightPunch || enemy.AfRightPunch || enemy.AfCouterRightPunch)
        {

            Vector3 exvec = new Vector3(0.254000008f, -0.0140000004f, 0.00300000003f);
            Vector3 crivec = new Vector3(-0.670000017f, -0.101999998f, -0.0149999997f);

            Vector3 explosionPosition = righthand.TransformPoint(exvec);
            GameObject Hit_exp = Instantiate(Hit_effect, explosionPosition, Quaternion.identity);
            Hit_exp.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            GameObject Hit_scr = Instantiate(Hit, spine2);


            Hit_scr.transform.localPosition = crivec;
            Hit_scr.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            ParticleSystem explosionParticle = Hit_exp.GetComponent<ParticleSystem>();
            if (explosionParticle != null)
            {
                explosionParticle.Play();
            }

            ParticleSystem criticalParticle = Hit_scr.GetComponent<ParticleSystem>();
            if (criticalParticle != null)
            {
                criticalParticle.Play();
            }

            audiosource.PlayOneShot(Hit_sound);
            
            Destroy(Hit_scr, 3f);
            Destroy(Hit_exp, 3f);



        }
        else if (enemy.IsCounterLeft || enemy.AFCounterLeftPunch)
        {

            Vector3 exvec = new Vector3(0.254000008f, -0.0140000004f, 0.00300000003f);
            Vector3 crivec = new Vector3(-0.670000017f, -0.101999998f, -0.0149999997f);

            Vector3 explosionPosition = righthand.TransformPoint(exvec);
            GameObject StrongHit_exp = Instantiate(StrongHit_effect, explosionPosition, Quaternion.identity);
            StrongHit_exp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            GameObject StrongHit_scr = Instantiate(StrongHit, spine2);


            StrongHit_scr.transform.localPosition = crivec;
            StrongHit_scr.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            ParticleSystem explosionParticle = StrongHit_exp.GetComponent<ParticleSystem>();
            if (explosionParticle != null)
            {
                explosionParticle.Play();
            }

            ParticleSystem criticalParticle = StrongHit_scr.GetComponent<ParticleSystem>();
            if (criticalParticle != null)
            {
                criticalParticle.Play();
            }

            audiosource.PlayOneShot(StrongHit_sound);
            
            Destroy(StrongHit_exp, 3f);
            Destroy(StrongHit_scr, 3f);

        }
        else if (enemy.IsCounterRight)
        {
            Vector3 missvec = new Vector3(-0.670000017f, -0.101999998f, -0.0149999997f);
            GameObject Miss_scr = Instantiate(Miss, spine2);
            Miss_scr.transform.localPosition = missvec;
            Miss_scr.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            ParticleSystem criticalParticle = Miss_scr.GetComponent<ParticleSystem>();
            if (criticalParticle != null)
            {
                criticalParticle.Play();
            }
            Destroy(Miss_scr, 3f);
        }
        else if (enemy.IsBlocking)
        {
            Vector3 guarvec = new Vector3(-0.670000017f, -0.101999998f, -0.0149999997f);
            GameObject Guardins = Instantiate(Guard, spine2);
            Guardins.transform.localPosition = guarvec;
            Guardins.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            ParticleSystem criticalParticle = Guardins.GetComponent<ParticleSystem>();
            if (criticalParticle != null)
            {
                criticalParticle.Play();
            }
            audiosource.PlayOneShot(Guard_sound);
            Destroy(Guardins, 3f);

        }

    }

    public void fin()
    {
        
        fina = true;
    }
    public void counterleftfin()
    {
        counterleftpunching = false;
        aftercounterleftpunch = true;

    }

    public void counterrightfin()
    {
        counterrightpunching = false;
        aftercounterrightpunch = true;

    }

    public void lpunchfin()
    {

        leftpunching = false;
        afterleftpunch = true;

    }

    public void rpunchfin()
    {

        rightpunching = false;
        afterrightpunch = true;
    }

    public void judge()
    {
        Gamecontroller.judge();
    }

    public void down()
    {
        Gamecontroller.down();
    }


}
