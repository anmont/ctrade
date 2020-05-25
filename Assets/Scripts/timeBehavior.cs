using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class timeBehavior : MonoBehaviour
{
    public static float currentTimeScale;
    public static DateTime startDate = new DateTime(1375,1,1);
    public static DateTime gameDate =new DateTime(1375,1,1); 
    public static string gameDateString;
    public static float cumulativeTime; //TODO needs serialized in game saves!!!!
    public static float gameDays;
    public static int temp = 0;
    public static int lastTemp = 0;
    public static float lastTS = 1;
    // Start is called before the first frame update

    public int lastMonth = 1;
    public int lastSundayWeek;
    public int lastYear = 1375;
    public int lastDay = 1;
    public bool everyOtherDay = false;

    public static void changeTimeScale(float value)
    {
        Time.timeScale = value;
        currentTimeScale = value;

    }
    public void changedTimeScale(float value)
    {
        //Debug.Log("Original Value is " + value.ToString());
        //this is for the gui only call that cannot read the static method
        Time.timeScale = value;
        currentTimeScale = value;

        //Debug.Log("Time scale is now " + Time.timeScale.ToString());
        //Debug.Log("CurentTime scale is now " + currentTimeScale.ToString());


    }
    public static void addTime(float value)
    {
        cumulativeTime  += (value * currentTimeScale) ;
        temp = Mathf.RoundToInt(cumulativeTime);
        if (lastTemp != temp)
        {
            lastTemp = temp;
        }
        setGameDays();
    }

    public static void setGameDays ()
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

        //every other day piggyback

        //Daily Trigger
        if(gameDate.Day != lastDay)
        {
            // TODO start co-routine for job
            lastDay = gameDate.Day;
            globals.dailyTrigger();

            if (everyOtherDay == false)
            {
                everyOtherDay = true;
            }
            else
            {
                everyOtherDay = false;
                globals.everyOtherDayTrigger();
            }
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
