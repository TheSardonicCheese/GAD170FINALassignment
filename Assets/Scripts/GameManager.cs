﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemyList;
    public List<GameObject> enemiesToFight;
    List<int> storedPlayerStats;
    Transform storedPlayerTransform;

    public enum fieldType
    {
        spaghetti,
        broccoli,
        stew
    }

    public fieldType myType;

    public enum Worlds
    {
        OverWorld,
        BattleStage
    }

    private static GameManager gameManRef;

    private void Awake()
    {
        if (gameManRef == null)
        {
            gameManRef = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void Start()
    {
        LoadPlayerStuff(true);
    }

   public void GenerateEnemies(List<GameObject> enemies)
    {
        foreach(GameObject enemy in enemies)
        {
            enemiesToFight.Add(enemy);
        }
    }

    public void TravelToWorld(Worlds destination)
    {
        switch (destination)
        {
            case Worlds.OverWorld:
                //load overworld
                SavePlayerStuff(false);
                SceneManager.LoadScene("OverWorld");
                //LoadPlayerStuff(true);
                break;

            case Worlds.BattleStage:
                //go to battle scence
                //GenerateEnemies();
                SavePlayerStuff(true);
                SceneManager.LoadScene("BattleStage");
                //LoadPlayerStuff(false);
                break;
        }
    }
    void SavePlayerStuff(bool isFromOverworld)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(playerObj.name);
        if (isFromOverworld)
        {
            //save both location and rotation as seperate floats, using similar naming conventions
            PlayerPrefs.SetFloat("playerPosx", playerObj.transform.position.x);
            PlayerPrefs.SetFloat("playerPosy", playerObj.transform.position.y);
            PlayerPrefs.SetFloat("playerPosz", playerObj.transform.position.z);
            PlayerPrefs.SetFloat("playerRotx", playerObj.transform.rotation.x);
            PlayerPrefs.SetFloat("playerRoty", playerObj.transform.rotation.y);
            PlayerPrefs.SetFloat("playerRotz", playerObj.transform.rotation.z);
        }

        //Save stats that we need to track!
        Stats playerStats = playerObj.GetComponent<Stats>();
        PlayerPrefs.SetFloat("playerHealth", playerStats.maxSatiety);
        PlayerPrefs.SetFloat("playerSatiety", playerStats.satiety);
        PlayerPrefs.SetInt("playerMetabolism", playerStats.metabolism);
        PlayerPrefs.SetInt("playerHunger", playerStats.hunger);
        PlayerPrefs.SetInt("playerRawness", playerStats.rawness);
        PlayerPrefs.SetInt("playerDexterity", playerStats.dexterity);
        PlayerPrefs.SetInt("playerLuck", playerStats.luck);
        PlayerPrefs.SetInt("playerCurrentLevel", playerStats.currentLevel);
        PlayerPrefs.SetFloat("playerXP", playerStats.totalXP);
        Debug.Log(playerStats.totalXP + " " + PlayerPrefs.HasKey("playerXP"));

    }

    public void LoadPlayerStuff(bool goingToOverworld)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        Debug.Log(playerObj.name);
        //load the existing stats and apply them to the player!
        Stats playerStats = playerObj.GetComponent<Stats>();
        playerStats.maxSatiety = PlayerPrefs.GetFloat("playerHealth", playerStats.maxSatiety);
        playerStats.satiety = PlayerPrefs.GetFloat("playerSatiety", playerStats.satiety);
        playerStats.metabolism = PlayerPrefs.GetInt("playerMetabolism", playerStats.metabolism);
        playerStats.hunger = PlayerPrefs.GetInt("playerHunger", playerStats.hunger);
        playerStats.rawness = PlayerPrefs.GetInt("playerRawness", playerStats.rawness);
        playerStats.dexterity = PlayerPrefs.GetInt("playerDexterity", playerStats.dexterity);
        playerStats.luck = PlayerPrefs.GetInt("playerLuck", playerStats.luck);
        playerStats.currentLevel = PlayerPrefs.GetInt("playerCurrentLevel", 1);
        playerStats.totalXP = PlayerPrefs.GetFloat("playerXP", playerStats.totalXP);
        Debug.Log(PlayerPrefs.GetInt("playerXP", 0) + " " + PlayerPrefs.HasKey("playerXP"));

        if (goingToOverworld)
        {
            playerObj.transform.position = new Vector3(PlayerPrefs.GetFloat("playerPosx", 40f),
                                                       PlayerPrefs.GetFloat("playerPosy", 11f),
                                                       PlayerPrefs.GetFloat("playerPosz", 28f));

            playerObj.transform.rotation = Quaternion.Euler(PlayerPrefs.GetFloat("playerRotx", 0f),
                                                       PlayerPrefs.GetFloat("playerRoty", 0f),
                                                       PlayerPrefs.GetFloat("playerRotz", 0f));
        }
       

    }

    public void DeleteSavedStuff()
    {
        //hard reset
        if (Input.GetKeyDown("k"))
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene("Overworld");
        }
     
    }
}