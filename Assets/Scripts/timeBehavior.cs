using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class timeBehavior : MonoBehaviour
{
    public float currentTimeScale;
    public static DateTime startDate = new DateTime(1375,1,1);
    public static DateTime gameDate =new DateTime(1375,1,1); 
    public static string gameDateString;
    public static float cumulativeTime; //TODO needs serialized in game saves!!!!
    public float gameDays;
    public int temp = 0;
    public int lastTemp = 0;
    public static float lastTS = 1;
    // Start is called before the first frame update

    public int lastMonth = 1;
    public int lastSundayWeek;
    public int lastYear = 1375;
    public int lastDay = 1;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (currentTimeScale == 0)
            {
                changeTimeScale(lastTS);
                //change back to previoous time
            }
            else
            {
                //record previous time
                lastTS = currentTimeScale; 
                changeTimeScale(0);

            }
        }
        //Weekly trigger
        if(gameDate.DayOfWeek == DayOfWeek.Monday)
        {
            //if this monday
            if (gameDate.Day != lastSundayWeek)
            {
                // TODO start co-routine for job
                lastSundayWeek = gameDate.Day;
                globals.weeklyTrigger();
            }
        }

        //Daily Trigger
        if(gameDate.Day != lastDay)
        {
            // TODO start co-routine for job
            lastDay = gameDate.Day;
            globals.dailyTrigger();
        }

        //Monthly trigger
        if(gameDate.Month != lastMonth)
        {
            lastMonth = gameDate.Month ;
            globals.monthlyTrigger();
        }

        //Yearly trigger
        if(gameDate.Year != lastYear)
        {
            lastYear = gameDate.Year ;
            globals.yearlyTrigger();
        }
    }
}
