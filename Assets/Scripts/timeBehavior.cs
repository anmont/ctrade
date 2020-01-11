using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class timeBehavior : MonoBehaviour
{
    public float currentTimeScale;
    public static DateTime startDate = new DateTime(1375,1,1);
    public static DateTime gameDate =new DateTime(1375,1,1); 
    public string gameDateString;
    public float cumulativeTime; //TODO needs serialized in game saves!!!!
    public float gameDays;
    public int temp = 0;
    public int lastTemp = 0;
    // Start is called before the first frame update

    public void changeTimeScale(float value)
    {
        Time.timeScale = value;
        currentTimeScale = value;

    }
    public void addTime(float value)
    {
        cumulativeTime  += (value * currentTimeScale) ;
        temp = Mathf.RoundToInt(cumulativeTime);
        if (lastTemp != temp)
        {
            lastTemp = temp;
        }
        setGameDays();
    }

    public void setGameDays ()
    {
        gameDays = Mathf.RoundToInt(cumulativeTime / 60);
        gameDate = startDate.AddDays(gameDays);
        //gameDate.AddDays(gameDays);
        gameDateString = (gameDate.Month + "/" + gameDate.Day + "/" + gameDate.Year);

    }

    void Start()
    {
        globals.time = this.gameObject;
        changeTimeScale(1f);
        
    }

    // Update is called once per frame
    void Update()
    {
        addTime(Time.deltaTime);
    }
}
