using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float maxSatiety;
    public float satiety;
    //treated as health
    public int metabolism;
    //treated as speed
    public int hunger;
    //treated as attack
    public int rawness;
    //treated as defense
    public int dexterity;
    //treated as accuracy/evasion
    public int luck;
    //treated as luck
    public int currentLevel;
    public float totalXP;
    public float xpRequired;
    public float xpGained;

    public bool isDefeated;
    public bool isSeasoned = false;

    private void Update()
    {
        if (satiety > maxSatiety)
        {
            satiety = maxSatiety;
        }
    }


    public void Attacked(int incDmg, int atkrDex, int atkrLuck)
    {
        //(attacker dex/ defender dex) * attacker luck
       
        if (Random.Range(1,100) <= ((atkrDex / dexterity) * atkrLuck ))
        {
            satiety -= incDmg - (incDmg * (100 / (rawness + 100)));
           
        }
        else
        {
            Debug.Log(" The attack missed!");
        }
       
        if (satiety <= 0)
            isDefeated = true;
    }

    public void SeasoningEffect()
    {
        if(isSeasoned == false)
        {
            rawness = rawness / 2;
            hunger = hunger / 2;
            Debug.Log("mmm looking salty");
            isSeasoned = true;
        }
        else
        {
            Debug.Log("Don't over spice!");
        }
    }
    void XPandLvlUp()
    {
        if (currentLevel >= 5)
        {
            xpRequired = currentLevel * currentLevel + 3;
        }
        else
        {
            xpRequired = currentLevel * 3 + 4;
        }

        if (totalXP >= xpRequired)
        {
            currentLevel++;
            maxSatiety += 20;
            satiety += 20;
            metabolism += 10;
            hunger += 10;
            rawness += 10;
            luck += 3;
            Debug.Log("Level Up!");
        }
    }

    public void GainXPGnoc()
    {
        totalXP += 2f / (.5f * currentLevel);
        XPandLvlUp();

    }
    public void GainXPRav()
    {
        totalXP += 5f / (.5f * currentLevel);
        XPandLvlUp();
    }
    public void GainXPMon()
    {
        totalXP += 10f / (.5f * currentLevel);
        XPandLvlUp();
    }

}