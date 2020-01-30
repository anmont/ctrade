using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cityEconomy : MonoBehaviour
{

    public List<int> cityProducers = new List<int>() { 0,0,0,1,1,1,2,2,2,3,3,3,4,4,4,5,5,5,6,6,6,7,7,7,8,8,8,9,9,9,10,10,10,11,11,11,12,12,12,13,13,13,14,14,14,15,15,15,16,16,16,17,17,17 };
    public List<int> cityPops = new List<int>() { 5000,7000,9000,10000,12000,12000,14000,15000,16000,17000 };


//maintain the moving averages of the Properity/Wealth/Growth
//Apply the growth to the actual population
    public void calculateGrowth()
    {
        //Get moving Average of Prosperity and update
        //Calculate the amount of food the city has for 
        //public List<double> cityProsperityFloat = new List<double>();
        //Get moving Average of Wealth and update
        //Calculate Growth

        foreach (GameObject city in globals.cityList)
        {
            //find the grain quantity / lastconsumption
            //if more than 10 days of food is in stock, then its 100% happiness... else its on a scale div by 10days
            double grainQty = city.GetComponent<uiCityScript>().cityInventory[0].quantity;
            double grainLastCon = city.GetComponent<uiCityScript>().cityInventory[0].lastConsumptionQty;
            double currentProsperity = 0;
            double daysOfConsumption = grainQty / grainLastCon;

            if (daysOfConsumption > 0)
            {
                if (daysOfConsumption > 9)
                {
                    currentProsperity = 1;
                }
                else
                {
                    currentProsperity = daysOfConsumption / 10;
                }
            }
            else
            {
                currentProsperity = 0;
            }

            //sum of all the 3 production counts divided by 3 then divided by 1000, then minus 1 (the closer to 0 the more prosperous)
            double prod1 = city.GetComponent<uiCityScript>().cityInventory[city.GetComponent<uiCityScript>().productionMaterial1].quantity;
            double prod2 = city.GetComponent<uiCityScript>().cityInventory[city.GetComponent<uiCityScript>().productionMaterial2].quantity;
            double prod3 = city.GetComponent<uiCityScript>().cityInventory[city.GetComponent<uiCityScript>().productionMaterial3].quantity;
            double prodAverageStock = ((prod1 + prod2 + prod3)/3);
            double currentWealth = 0;

            if (prodAverageStock < 1000)
            {
                currentWealth = 1-(prodAverageStock / 1000);
            }

            double currentGrowth = currentWealth + currentProsperity;

            //update all floating averages
            city.GetComponent<uiCityScript>().cityProsperityFloat.Add(currentProsperity);
            city.GetComponent<uiCityScript>().cityWealthFloat.Add(currentWealth);
            city.GetComponent<uiCityScript>().cityGrowthFloat.Add(currentGrowth);

            //remove 11th average
            if (city.GetComponent<uiCityScript>().cityProsperityFloat.Count > 10)
            {
                city.GetComponent<uiCityScript>().cityProsperityFloat.RemoveAt(0);
            }
            if (city.GetComponent<uiCityScript>().cityWealthFloat.Count > 10)
            {
                city.GetComponent<uiCityScript>().cityWealthFloat.RemoveAt(0);
            }
            if (city.GetComponent<uiCityScript>().cityGrowthFloat.Count > 10)
            {
                city.GetComponent<uiCityScript>().cityGrowthFloat.RemoveAt(0);
            }

            //Set final 10 day average values
            double pTot = 0;
            double wTot = 0;
            double gTot = 0;

            foreach (double item in city.GetComponent<uiCityScript>().cityProsperityFloat)
            {
                pTot += item;
            }
            city.GetComponent<uiCityScript>().cityProsperity = (pTot / city.GetComponent<uiCityScript>().cityProsperityFloat.Count);

            foreach (double item in city.GetComponent<uiCityScript>().cityWealthFloat)
            {
                wTot += item;
            }
            city.GetComponent<uiCityScript>().cityWealth = (wTot / city.GetComponent<uiCityScript>().cityWealthFloat.Count);

            foreach (double item in city.GetComponent<uiCityScript>().cityGrowthFloat)
            {
                gTot += item;
            }
            city.GetComponent<uiCityScript>().cityGrowth = (gTot / city.GetComponent<uiCityScript>().cityGrowthFloat.Count);
            //foreach (tradeGoods product in city.GetComponent<uiCityScript>().cityInventory)
            //{

            //}
        }

    }

    public void calculateProduction()
    {
        foreach (GameObject city in globals.cityList)
        {
            int population = city.GetComponent<uiCityScript>().cityPopulation;
            int product1 = city.GetComponent<uiCityScript>().productionMaterial1;
            int product2 = city.GetComponent<uiCityScript>().productionMaterial2;
            int product3 = city.GetComponent<uiCityScript>().productionMaterial3;

            int i = 0;
           
            foreach (tradeGoods product in city.GetComponent<uiCityScript>().cityInventory)
            {
                double productionMod = 0.75;
                if (i == product1 || i == product2 || i == product3 )
                {
                    productionMod = 1.5;
                }

                double dailyProduction = (double)population/(365/product.perPersonPerYear)*productionMod;
                product.lastProductionQty = dailyProduction;
                product.quantity = product.quantity + dailyProduction;
                i++;
            }
        }

    }

    public void calculateConsumption()
    {
        foreach (GameObject city in globals.cityList)
        {
            int population = city.GetComponent<uiCityScript>().cityPopulation;
            int i = 0;
           
            foreach (tradeGoods product in city.GetComponent<uiCityScript>().cityInventory)
            {
                double dailyConsumption = (double)population/(365/product.perPersonPerYear);
                product.lastConsumptionQty = dailyConsumption;
                product.quantity = product.quantity - dailyConsumption;
                i++;
            }
        }

    }
    
    public void determinePopulation(GameObject city)
    {
        int randMe = Random.Range(0,cityPops.Count -1);
        city.GetComponent<uiCityScript>().cityPopulation = cityPops[randMe];
        cityPops.RemoveAt(randMe);
        
    }
    public void determineProduction(GameObject city)
    {
        //determine the list of city production (Production 1,2,and 3 from the list of production)
        int rand1 = 50;
        int rand2 = 50;
        int rand3 = 50;

        int randMe = Random.Range(0,cityProducers.Count -1);
        rand1 = cityProducers[randMe];
        cityProducers.RemoveAt(randMe);

        int loopCnt1 = 0;
        while (rand2 == 50)
        {
            int randMe2 = Random.Range(0,cityProducers.Count -1);
            if (rand1 != cityProducers[randMe2])
            {
                rand2 = cityProducers[randMe2];
                cityProducers.RemoveAt(randMe2);
            }
            if (loopCnt1 > 5)
            {
                rand2 = 0;
            }
            loopCnt1++;
        }

        int loopCnt2 = 0;
        while (rand3 == 50)
        {
            int randMe3 = Random.Range(0,cityProducers.Count -1);
            if (rand1 != cityProducers[randMe3])
            {
                if(rand2 != cityProducers[randMe3])
                {
                    rand3 = cityProducers[randMe3];
                    cityProducers.RemoveAt(randMe3);
                }
            }
            if (loopCnt2 > 5)
            {
                rand3 = 0;
            }
            loopCnt2++;
        }

        city.GetComponent<uiCityScript>().productionMaterial1 = rand1;
        city.GetComponent<uiCityScript>().productionMaterial2 = rand2;
        city.GetComponent<uiCityScript>().productionMaterial3 = rand3;


    }

    void Start() {
        globals.cityEconomyInstance = this.gameObject;
    }

}
