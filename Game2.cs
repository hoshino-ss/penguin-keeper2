using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class Game2 : MonoBehaviour
{
    //System//
    //Lognin
    DateTime nowDate;
    //Hp

    //数値//
    //Login
    public int ts;
    int tts;
    int todayDate;
    int lastDate;
    //HP
    int hp;
    int newHp;
    int hpId;
    int maxHp = 14400;
    float hpGage;
    //個体数
    public static int posChild = 0;

    //Object//
    public GameObject o1; //主人公
    public GameObject egg; //主人公egg
    public Slider slider;　//hpゲージ

    //Text//
    public Text gettext;　//報酬


    void Start()
    {
        SaveData();
        Login();
        //LoginTrial();
        cHp();
        Hp();
        HpGage();
        Egg();
        DateSave();
    }

    void SaveData()
    {
        hp = PlayerPrefs.GetInt("LastHp", hp);
        newHp = PlayerPrefs.GetInt("NewHp", newHp);
        hpId = PlayerPrefs.GetInt("HpId", hpId);
        tts = PlayerPrefs.GetInt("TotalTimeSpan", tts);
        lastDate = PlayerPrefs.GetInt("LastGetDate", todayDate);
        posChild = PlayerPrefs.GetInt("Child", posChild);
    }

    void Login()
    {
        nowDate = DateTime.Now;
        todayDate = nowDate.Year * 525600 + nowDate.Month * 43800 + nowDate.Day * 1440 + nowDate.Hour * 60 + nowDate.Minute;
        ts = todayDate - lastDate;
        tts = tts + ts;
        hp = hp - ts;
        Debug.Log("today" + todayDate);
        Debug.Log("lastday" + lastDate);
    }

    void LoginTrial()
    {
        tts = tts + ts;
        hp = hp - ts;

    }


    void Hp()
    {
        //hp結果
        if (hp > 0)
        {
            GetPoint();
        }
        else if (hp <= 0)
        {
            GetDie();
            hp = 14400;
            HpGage();
        }

    }

    void HpGage() //Hpgage
    {
        hpGage = (float)hp / (float)maxHp;
        Debug.Log("hp" + hp);
        Debug.Log("hpgage" + hpGage);
    }

    void cHp()
    {
        if (newHp < hp)
        {
            return;
        }
        else
        {
            newHp = hp;
        }
        Debug.Log("newhp" + newHp);

    }

    void Egg() //tts 経過時間と卵の成長
    {
        Debug.Log("tts" + tts);

        if (tts <= 2880) //~2日
        {
            egg.SetActive(false);
        }
        else if ((2880 < tts) && (tts < 5760)) //2~4日
        {
            egg.SetActive(true);
            gettext.text += "+egg";
        }
        else if (tts >= 5760) //4日~
        {
            Child();
            egg.SetActive(false);
            tts = 1440;
            newHp = 14400;
            gettext.text += "\nGetBaby";

        }
    }

    void Child() //newHp 放置日数と子供の数
    {
        if (newHp <= 10080) //3日~放置
        {
            posChild += 1;
            gettext.text += "\n1child";
        }
        else if ((newHp > 10080) && (newHp < 12960)) //1日~2日放置
        {
            posChild += 2;
            gettext.text += "\n2child";
        }
        else if (newHp >= 12960) //~1日放置
        {
            posChild += 3;
            gettext.text += "\n3child";
        }

    }

    void DateSave()
    {
        PlayerPrefs.SetInt("LastHp", hp);
        PlayerPrefs.SetInt("LastGetDate", todayDate);
        PlayerPrefs.SetInt("TotalTimeSpan", tts);
        PlayerPrefs.SetInt("NewHp", newHp);
        PlayerPrefs.SetInt("Child", posChild);
        PlayerPrefs.Save();

    }

    void TextUI()
    {
    }

    void Update()
    {
        slider.value = hpGage;
    }


    void GetPoint() //ゲームクリア報酬
    {
        gettext.text = "alive";
    }

    void GetDie() //ゲームオーバー
    {
        gettext.text = "die";
    }

    public void CloseGetPoint() //報酬(閉じる)
    {
    }

    public void AfterFishye() //エサやる
    {
        gettext.text = "fishye\nfullHP";

        hp = 14400;
        PlayerPrefs.SetInt("LastHp", hp);
        PlayerPrefs.Save();
        Debug.Log("hp" + hp);
        HpGage();
    }

    public void Game1Child()
    {
        FadeManager.Instance.LoadScene("Game1Child", 2.0f);
    }

}