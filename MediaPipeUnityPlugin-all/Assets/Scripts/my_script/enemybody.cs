using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Net.Http.Headers;
using System.ComponentModel.Design;

public class enemybody : MonoBehaviour
{
    // Start is called before the first frame update
    
    private Animator animator;
    public Transform player;
    public GameObject explosion_right;
    public GameObject Critical_right;
    public Transform spine2;
    public Transform lefthand;
    public Transform righthand;
    public bodymode enemy;
    public GameObject Guard;
    public GameObject Hit;
    public GameObject Hit_effect;
    public GameObject StrongHit;
    public GameObject StrongHit_effect;
    public GameObject Miss;
    public hpscript hp1;
    public enemyhpscripthpscript hp2;
    public GameTimer gametimer;
    public GameObject rightwaring;
    public GameObject leftwaring;
    public Gamecontroller Gamecontroller;
    public AudioClip punch;
    public AudioClip Hit_sound;
    public AudioClip StrongHit_sound;
    public AudioClip Critical_sound;
    public AudioClip Guard_sound;
    AudioSource audiosource;
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
    private int count = 0;

    public bool IsRightPunch => rightpunching;
    public bool IsLeftPunch => leftpunching;
    public bool IsCounterRight => counterrightpunching;
   
    public bool IsCounterLeft => counterleftpunching;
    public bool AfRightPunch => afterrightpunch;
    public bool AfLeftPunch => afterleftpunch;
    public bool AfCouterRightPunch => aftercounterrightpunch;
    public bool AFCounterLeftPunch => aftercounterleftpunch;
    public bool IsBlocking => blocking;
    Vector3 defaultposition = new Vector3(4.96999979f, 0f, -2.46000004f);

    private bool fina = true;
    /// <summary>
    /// /////////////////////////////////////////////////////
    /// </summary>
    /*enemy acition setting*/
    private int punchprobability = 20;
    private int counterprobability = 10;

    private int punch_punchprobability = 10;
    private int punch_suc_counterprobability = 20;
    private int punch_false_counterprobability = 10;
    private float duration = 0.8f;
    /*予備動作の早さとか*/
    private int counter_suc_punchprobability =70;
    private int counter_false_punchprobability = 5;
    private int counter_counter_probability = 10;
    private bool ishell = false;
   
    





    public float actionInterval = 0.4f; // 行動切り替えの間隔
    private float timer = 0f;
    
    void Awake()
    {
        audiosource = GetComponent<AudioSource>();
        int level = PlayerPrefs.GetInt("level",4);
        switch (level)
        {
            case 1:
                punchprobability = 40;
                counterprobability = 15;
                punch_punchprobability = 5;
                punch_suc_counterprobability = 5;
                punch_false_counterprobability = 60;
                duration = 0.5f;
                break;
            case 2:
                punchprobability = 40;
                counterprobability = 15;
                punch_punchprobability = 20;
                punch_suc_counterprobability = 10;
                punch_false_counterprobability = 15;
                duration = 0.5f;
                break;
            case 3:
                punchprobability = 35;
                counterprobability = 5;
                punch_punchprobability = 10;
                punch_suc_counterprobability = 30;
                punch_false_counterprobability = 10;
                duration = 0.43f;
                break;
            case 4:
                punchprobability =35;
                counterprobability = 0;
                punch_punchprobability = 5;
                punch_suc_counterprobability = 50;
                punch_false_counterprobability = 0;
                duration = 0.29f;
                ishell = true;
                break;
        }
            
    }
    void Start()
    {
        animator = GetComponent<Animator>();
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
    count = 0;
    timer = 0f;
    player.transform.rotation = Quaternion.Euler(0f, 17.253f, 0f);
    player.transform.position = defaultposition;
    Awake();
    Start();
  }

  // Update is called once per frame
  void Update()
    {
        
        
        if (Gamecontroller.Stop)
        {
            return;
        }
        if (IsPlayingAnimation())
        {
            if(counterleftpunch == true)
            {
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation,Quaternion.Euler(0f, 45.153f, 0f), Time.deltaTime * 5f);
            }
            else if(counterrightpunch == true)
            {
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0f, -45.153f, 0f), Time.deltaTime * 5f);

            }else if(leftpunch == true)
            {
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0f, -17.253f, 0f), Time.deltaTime * 5f);
            }
               
            return;
        }
        else
        {
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0f, 17.253f, 0f), Time.deltaTime * 5f);
            player.transform.position = Vector3.MoveTowards(player.transform.position, defaultposition, Time.deltaTime * 5f);
            blocking = true;
            afterrightpunch = false;
            afterleftpunch = false;
            aftercounterleftpunch = false;
            aftercounterrightpunch = false;
            
        }

   
        timer += Time.deltaTime;
        if (((((timer > actionInterval) || enemy.IsLeftPunch || enemy.IsRightPunch) && count == -1) || ((timer > actionInterval) && count == 0)) && fina)
        {
           
            if (hp1.currentRate>0 && hp2.currentRate >0 && gametimer.Timer > 0)
            {
                
                DecideNextAction();
            }
            
            timer = 0f;
        }

        //if (((((timer > actionInterval) || enemy.IsLeftPunch || enemy.IsRightPunch) && count == 0) || ((timer > actionInterval) && count == -1)) && fina)
        //{

        //    if (hp1.currentRate > 0 && hp2.currentRate > 0 && gametimer.Timer > 0)
        //    {
        //        DecideNextAction();
        //    }

        //    timer = 0f;
        //}



    }
    

    void DecideNextAction()
    {
        int rand = Random.Range(0, 100);

        count++;
        if (enemy.IsRightPunch)
        {
            
            
            if (0 <= rand && rand < punch_punchprobability)
            {
                fina = false;
                StartCoroutine(RightPunch());
            }
            else if (punch_punchprobability <= rand && rand < (2 * punch_punchprobability))
            {
                fina = false;
                StartCoroutine(LeftPunch());
            }
            else if ((2 * punch_punchprobability) <= rand && rand < (2 * punch_punchprobability + punch_suc_counterprobability))
            {
                rightpunch = false;
                leftpunch = false;
                counterrightpunch = true;
                counterleftpunch = false;
                blocking = false;
                counterrightpunching = true;
                fina = false;
                StartCoroutine(PunchPlayDelayedSound(0.4f));
                animator.SetTrigger("counterrightpunch");
            }
            else if ((2 * punch_punchprobability + punch_suc_counterprobability) <= rand && rand < (2 * punch_punchprobability + punch_false_counterprobability + punch_suc_counterprobability))
            {
                rightpunch = false;
                leftpunch = false;
                counterrightpunch = false;
                counterleftpunch = true;
                blocking = false;
                counterleftpunching = true;
                fina = false;
                StartCoroutine(PunchPlayDelayedSound(0.4f));
                animator.SetTrigger("counterleftpunch");

            }
            else
            {
                rightpunch = false;
                leftpunch = false;
                counterleftpunch = false;
                counterrightpunch = false;
                blocking = true;
                count = -1;
            }
        }
        else if (enemy.IsLeftPunch)
        {
            
            if (0 <= rand && rand < punch_punchprobability)
            {
                fina = false;
                StartCoroutine(RightPunch());
            }
            else if (punch_punchprobability <= rand && rand < (2 * punch_punchprobability))
            {
                fina = false;
                StartCoroutine(LeftPunch());
            }
            else if ((2 * punch_punchprobability) <= rand && rand < (2 * punch_punchprobability + punch_false_counterprobability))
            {
                rightpunch = false;
                leftpunch = false;
                counterrightpunch = true;
                counterleftpunch = false;
                blocking = false;
                counterrightpunching = true;
                fina = false;
                StartCoroutine(PunchPlayDelayedSound(0.4f));
                animator.SetTrigger("counterrightpunch");
            }
            else if ((2 * punch_punchprobability + punch_false_counterprobability) <= rand && rand < (2 * punch_punchprobability + punch_suc_counterprobability + punch_false_counterprobability))
            {
                rightpunch = false;
                leftpunch = false;
                counterrightpunch = false;
                counterleftpunch = true;
                blocking = false;
                counterleftpunching = true;
                fina = false;
                StartCoroutine(PunchPlayDelayedSound(0.4f));
                animator.SetTrigger("counterleftpunch");

            }
            else
            {
                rightpunch = false;
                leftpunch = false;
                counterleftpunch = false;
                counterrightpunch = false;
                blocking = true;
                count = -1;
            }
        }
        else if (enemy.IsCounterLeft && ishell)
        {
            print("Left");
            if (0 <= rand && rand < counter_counter_probability)
            {
                rightpunch = false;
                leftpunch = false;
                counterrightpunch = true;
                counterleftpunch = false;
                blocking = false;
                counterrightpunching = true;
                fina = false;
                StartCoroutine(PunchPlayDelayedSound(0.4f));
                animator.SetTrigger("counterrightpunch");
            }
            else if (counter_counter_probability <= rand && rand < 2 * counter_counter_probability)
            {
                rightpunch = false;
                leftpunch = false;
                counterrightpunch = false;
                counterleftpunch = true;
                blocking = false;
                counterleftpunching = true;
                fina = false;
                StartCoroutine(PunchPlayDelayedSound(0.4f));
                animator.SetTrigger("counterleftpunch");
            }
            else if (2 * counter_counter_probability <= rand && rand < 2 * counter_counter_probability + counter_suc_punchprobability)
            {
                //StartCoroutine(RightPunch_forcounter());
                rightpunch = true;
                leftpunch = false;
                counterleftpunch = false;
                counterrightpunch = false;
                blocking = false;
                rightpunching = true;
                fina = false;

                animator.SetTrigger("erightpunch");
            }
            else if (2 * counter_counter_probability + counter_suc_punchprobability <= rand && rand < 2 * counter_counter_probability + counter_suc_punchprobability + counter_false_punchprobability)
            {
                //StartCoroutine(LeftPunch_forcounter());
                rightpunch = false;
                leftpunch = true;
                counterleftpunch = false;
                counterrightpunch = false;
                blocking = false;
                leftpunching = true;
                fina = false;


                animator.SetTrigger("eleftpunch");
            }
            else
            {
                rightpunch = false;
                leftpunch = false;
                counterleftpunch = false;
                counterrightpunch = false;
                blocking = true;
                count = -1;
            }

        }
        else if (enemy.IsCounterRight && ishell)
        {
            print("right");
            if (0 <= rand && rand < counter_counter_probability)
            {
                rightpunch = false;
                leftpunch = false;
                counterrightpunch = true;
                counterleftpunch = false;
                blocking = false;
                counterrightpunching = true;
                fina = false;
                StartCoroutine(PunchPlayDelayedSound(0.4f));
                animator.SetTrigger("counterrightpunch");
            }
            else if (counter_counter_probability <= rand && rand < 2 * counter_counter_probability)
            {
                rightpunch = false;
                leftpunch = false;
                counterrightpunch = false;
                counterleftpunch = true;
                blocking = false;
                counterleftpunching = true;
                fina = false;
                StartCoroutine(PunchPlayDelayedSound(0.4f));
                animator.SetTrigger("counterleftpunch");
            }
            else if (2 * counter_counter_probability <= rand && rand < 2 * counter_counter_probability + counter_suc_punchprobability)
            {
                //StartCoroutine(LeftPunch_forcounter());
                rightpunch = false;
                leftpunch = true;
                counterleftpunch = false;
                counterrightpunch = false;
                blocking = false;
                leftpunching = true;
                fina = false;


                animator.SetTrigger("eleftpunch");
            }
            else if (2 * counter_counter_probability + counter_suc_punchprobability <= rand && rand < 2 * counter_counter_probability + counter_suc_punchprobability + counter_false_punchprobability)
            {
                //StartCoroutine(RightPunch_forcounter());
                rightpunch = true;
                leftpunch = false;
                counterleftpunch = false;
                counterrightpunch = false;
                blocking = false;
                rightpunching = true;
                fina = false;

                animator.SetTrigger("erightpunch");
            }
            else
            {
                rightpunch = false;
                leftpunch = false;
                counterleftpunch = false;
                counterrightpunch = false;
                blocking = true;
                count = -1;
            }
        }
        else
        {

            if (0 <= rand && rand < punchprobability)
            {
                fina = false;
                StartCoroutine(RightPunch());
            }
            else if (punchprobability <= rand && rand < (2 * punchprobability))
            {
                fina = false;
                StartCoroutine(LeftPunch());
            }
            else if ((2 * punchprobability) <= rand && rand < (2 * punchprobability + counterprobability))
            {
                rightpunch = false;
                leftpunch = false;
                counterrightpunch = true;
                counterleftpunch = false;
                blocking = false;
                counterrightpunching = true;
                fina = false;
                StartCoroutine(PunchPlayDelayedSound(0.4f));
                animator.SetTrigger("counterrightpunch");
            }
            else if ((2 * punchprobability + counterprobability) <= rand && rand < (2 * (punchprobability + counterprobability)))
            {
                rightpunch = false;
                leftpunch = false;
                counterrightpunch = false;
                counterleftpunch = true;
                blocking = false;
                counterleftpunching = true;
                fina = false;
                StartCoroutine(PunchPlayDelayedSound(0.4f));
                animator.SetTrigger("counterleftpunch");

            }
            else
            {
                rightpunch = false;
                leftpunch = false;
                counterleftpunch = false;
                counterrightpunch = false;
                blocking = true;
                count = -1;
            }
        }

        

           

        
    }

    IEnumerator PunchPlayDelayedSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        audiosource.PlayOneShot(punch);
    }

    IEnumerator BlinkParticle(GameObject particlePrefab, Vector3 position, float duration, float interval, bool right)
    {
        if (right)
        {
            
            GameObject effect = Instantiate(particlePrefab, righthand);
            effect.transform.localPosition = position;
            effect.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            var ps = effect.GetComponent<ParticleSystem>();
            bool isOn = true;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                if (isOn) ps.Play();
                else ps.Stop();

                isOn = !isOn;
                elapsed += interval;
                yield return new WaitForSeconds(interval);
            }

            Destroy(effect);

        }
        else
        {
            GameObject effect = Instantiate(particlePrefab, lefthand);
            effect.transform.localPosition = position;
            effect.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            var ps = effect.GetComponent<ParticleSystem>();
            bool isOn = true;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                if (isOn) ps.Play();
                else ps.Stop();

                isOn = !isOn;
                elapsed += interval;
                yield return new WaitForSeconds(interval);
            }

            Destroy(effect);
        }
        
        

        
    }

    IEnumerator RightPunch()
    {


       
        // 右手用パーティクル点滅
        Vector3 warnPos = new Vector3(0f,0f,0f);
        yield return StartCoroutine(BlinkParticle(rightwaring, warnPos, duration, 0.1f,true));


        rightpunch = true;
        leftpunch = false;
        counterleftpunch = false;
        counterrightpunch = false;
        blocking = false;
        rightpunching = true;
        fina = false;

        StartCoroutine(PunchPlayDelayedSound(0.2f));
        animator.SetTrigger("erightpunch");
    }

    IEnumerator LeftPunch()
    {
        
        // 左手用パーティクル点滅
        Vector3 warnPos = new Vector3(0f,0f,0f);
        yield return StartCoroutine(BlinkParticle(leftwaring, warnPos, duration, 0.1f, false));

        rightpunch = false;
        leftpunch = true;
        counterleftpunch = false;
        counterrightpunch = false;
        blocking = false;
        leftpunching = true;
        fina = false;

        StartCoroutine(PunchPlayDelayedSound(0.2f));
        animator.SetTrigger("eleftpunch");
    }

    IEnumerator RightPunch_forcounter()
    {



        // 右手用パーティクル点滅
        Vector3 warnPos = new Vector3(0f, 0f, 0f);
        yield return StartCoroutine(BlinkParticle(rightwaring, warnPos, duration, 0.1f, true));


        rightpunch = true;
        leftpunch = false;
        counterleftpunch = false;
        counterrightpunch = false;
        blocking = false;
        rightpunching = true;
        fina = false;

        animator.SetTrigger("erightpunch");
    }

    IEnumerator LeftPunch_forcounter()
    {

        // 左手用パーティクル点滅
        Vector3 warnPos = new Vector3(0f, 0f, 0f);
        yield return StartCoroutine(BlinkParticle(leftwaring, warnPos, duration, 0.1f, false));


        rightpunch = false;
        leftpunch = true;
        counterleftpunch = false;
        counterrightpunch = false;
        blocking = false;
        leftpunching = true;
        fina = false;

       
        animator.SetTrigger("eleftpunch");
    }

    bool IsPlayingAnimation()
    {
        if (animator == null) return false;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime < 1f && stateInfo.loop == false;
    }

    public void counterl()
    {

        if (enemy.IsLeftPunch || enemy.AfLeftPunch)
        {
            Vector3 exvec = new Vector3(-0.0326f, 0.1372f, 0.0095f);
            Vector3 crivec = new Vector3(-0.0379999988f, 0.352f, 0.147f);

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



        }
        else if (enemy.IsRightPunch || enemy.AfRightPunch || enemy.IsCounterLeft || enemy.IsCounterRight || enemy.AfCouterRightPunch || enemy.AFCounterLeftPunch)
        {

            Vector3 exvec = new Vector3(-0.0326f, 0.1372f, 0.0095f);
            Vector3 crivec = new Vector3(-0.0379999988f, 0.352f, 0.147f);

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
            Vector3 guarvec = new Vector3(-0.0379999988f, 0.352f, 0.147f);
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
            Vector3 crivec = new Vector3(-0.0379999988f, 0.352f, 0.147f);

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
            Vector3 crivec = new Vector3(-0.0379999988f, 0.352f, 0.147f);

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
            Vector3 guarvec = new Vector3(-0.0379999988f, 0.352f, 0.147f);
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
            Vector3 crivec = new Vector3(-0.0379999988f, 0.352f, 0.147f);

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
        else if (enemy.IsCounterRight || enemy.AfCouterRightPunch)
        {

            Vector3 exvec = new Vector3(-0.0326f, 0.1372f, 0.0095f);
            Vector3 crivec = new Vector3(-0.0379999988f, 0.352f, 0.147f);

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

        }
        else if (enemy.IsCounterLeft)
        {
            Vector3 missvec = new Vector3(-0.0379999988f, 0.352f, 0.147f);
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
            Vector3 guarvec = new Vector3(-0.0379999988f, 0.352f, 0.147f);
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
            Vector3 crivec = new Vector3(-0.0379999988f, 0.352f, 0.147f);

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
            Vector3 crivec = new Vector3(-0.0379999988f, 0.352f, 0.147f);

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
            Vector3 missvec = new Vector3(-0.0379999988f, 0.352f, 0.147f);
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
            Vector3 guarvec = new Vector3(-0.0379999988f, 0.352f, 0.147f);
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
        count = 0;
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
}
