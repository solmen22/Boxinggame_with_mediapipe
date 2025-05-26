using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class hpscript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image enemyhealthImage;
    [SerializeField] private Image enemyburnImage;
    public enemybody enemy;
    
    public float duration = 0.5f;
    public float strength = 20f;
    public int vibrate = 100;

    public float debugDamageRate = 0.2f;
    public float currentRate = 1.0f;
    public float Maxhealth = 100f;
    public float Guarddamage = 2f;
    public float Hitdamage = 10f;
    public float Stronghitdamage = 20f;
    public float Criticaldamage = 30f;
    public Image hpBar;
    public System.Action OnHpAnimationComplete;

    private void Awake()
    {
        int level = PlayerPrefs.GetInt("level", 4);
        switch(level)
        {
            case 1:
                Maxhealth = 50f;
                break;
            case 2:
                Maxhealth = 100f;
                break;
            case 3:
                Maxhealth = 125f;
                break;
            case 4:
                Maxhealth = 150f;
                break;

        }
    }

  void OnEnable()
  {
    currentRate = 1.0f;
    enemyhealthImage.DOFillAmount(currentRate,0f);
    enemyburnImage.DOFillAmount(currentRate,0f);


    Awake();
  }
  public void SetGauge(float targetRate)
    {
        enemyhealthImage.DOFillAmount(targetRate, duration).OnComplete(() =>
        {
            enemyburnImage.DOFillAmount(targetRate, duration * 0.5f).SetDelay(0.2f);
            OnHpAnimationComplete?.Invoke(); // 外部に通知
        });
        hpBar.transform.DOShakePosition(duration * 0.5f, strength, vibrate);
        currentRate = targetRate;
    }

    public void TakeDamage(float rate)
    {
        SetGauge(currentRate - rate);
    }

    public float ChangeDamage(float Damage)
    {

        return Damage / Maxhealth;
    }
    public void counterl()
    {

        if (enemy.IsLeftPunch || enemy.AfLeftPunch)
        {
            
            TakeDamage(ChangeDamage(Criticaldamage));

        }
        else if (enemy.IsRightPunch || enemy.AfRightPunch || enemy.IsCounterLeft || enemy.IsCounterRight || enemy.AfCouterRightPunch || enemy.AFCounterLeftPunch)
        {

            TakeDamage(ChangeDamage(Hitdamage));
            
        }
        else if (enemy.IsBlocking)
        {
            TakeDamage(ChangeDamage(Guarddamage));


        }

    }

    public void counterr()
    {

        if (enemy.IsRightPunch || enemy.AfRightPunch)
        {
           
            TakeDamage(ChangeDamage(Criticaldamage));
           
        }
        else if (enemy.IsLeftPunch || enemy.AfLeftPunch || enemy.IsCounterLeft || enemy.IsCounterRight || enemy.AfCouterRightPunch || enemy.AFCounterLeftPunch)
        {

            
            TakeDamage(ChangeDamage(Hitdamage));
           

        }
        else if (enemy.IsBlocking)
        {
           
            TakeDamage(ChangeDamage(Guarddamage));
            

        }


    }

    public void punchl()
    {
        if (enemy.IsLeftPunch || enemy.AfLeftPunch || enemy.IsRightPunch || enemy.AfRightPunch || enemy.AFCounterLeftPunch)
        {

            
            TakeDamage(ChangeDamage(Hitdamage));
           



        }
        else if (enemy.IsCounterRight || enemy.AfCouterRightPunch)
        {

            
            TakeDamage(ChangeDamage(Stronghitdamage));
           

        }
        else if (enemy.IsCounterLeft)
        {
            
        }
        else if (enemy.IsBlocking)
        {
           
            TakeDamage(ChangeDamage(Guarddamage));

        }

    }

    public void punchr()
    {

        if (enemy.IsLeftPunch || enemy.AfLeftPunch || enemy.IsRightPunch || enemy.AfRightPunch || enemy.AfCouterRightPunch)
        {

            TakeDamage(ChangeDamage(Hitdamage));
            
        }
        else if (enemy.IsCounterLeft || enemy.AFCounterLeftPunch)
        {

           
            TakeDamage(ChangeDamage(Stronghitdamage));
           

        }
        else if (enemy.IsCounterRight)
        {
           
        }
        else if (enemy.IsBlocking)
        {
            
            TakeDamage(ChangeDamage(Guarddamage));

        }

    }
}
