using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    public static GamePanel instance;
    public Slider hpSlider;
    public Slider expSlider;
    public TMP_Text moneyCount;
    public TMP_Text expCount;
    public TMP_Text grade;
    public TMP_Text hpCount;
    public TMP_Text countDown;
    public TMP_Text waveCount;

    private void Awake()
    {
        instance = this;
        hpSlider = GameObject.Find("HpSlider").GetComponent<Slider>();
        expSlider = GameObject.Find("ExpSlider").GetComponent<Slider>();
        moneyCount = GameObject.Find("MoneyCount").GetComponent<TMP_Text>();
        expCount = GameObject.Find("ExpCount").GetComponent<TMP_Text>();
        grade = GameObject.Find("Grade").GetComponent<TMP_Text>();
        hpCount = GameObject.Find("HpCount").GetComponent<TMP_Text>();
        countDown = GameObject.Find("CountDown").GetComponent<TMP_Text>();
        waveCount = GameObject.Find("WaveCount").GetComponent<TMP_Text>();

    }

    // Start is called before the first frame update
    void Start()
    {
        RenewExp();
        RenewHp();
        RenewMoney();
        RenewWaveCount();
    }

    public void RenewHp()
    {
        hpCount.text = Player.Instance.hp + " / " + Player.Instance.maxHp;
        hpSlider.value = Player.Instance.hp / Player.Instance.maxHp;
    }

    public void RenewExp()
    {
        expSlider.value = Player.Instance.exp % 12 / 12f;
        expCount.text = Player.Instance.exp % 12 + " / 12";
        grade.text = "Lv." + Player.Instance.exp / 12;
    }

    public void RenewMoney()
    {
        moneyCount.text = Player.Instance.money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RenewCountDown(float timer)
    {
        countDown.text = timer.ToString("F0");
    }

    public void RenewWaveCount()
    {
        waveCount.text = "ตฺ " + GameManager.Instance.currentWave + " นุ";
        GameManager.Instance.currentWave++;
    }
}
