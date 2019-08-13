using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallGrass : MonoBehaviour
{
    private GameObject gameManager;
    public List<GameObject> enemiesLibrary;
    public GameObject player;

    public bool isInField;
    public enum fieldType
    {
        spaghetti,
        broccoli,
        stew
    }
    public fieldType myType;
        
    public List<GameObject> enemiesToSend;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        enemiesLibrary.Add(GameObject.FindGameObjectWithTag("Easy"));
        enemiesLibrary.Add(GameObject.FindGameObjectWithTag("Normal"));
        enemiesLibrary.Add(GameObject.FindGameObjectWithTag("Difficult"));
        //grab all the kinds of enemies
        
        RollDice();
    }



    void RollDice()
    {
        int diceRoll = Random.Range(1, 7);
        if (diceRoll > 2 && isInField)
        {
            for(int i = 0; i < 3; i++)
            {
                /*if (player.GetComponent<Stats>().currentLevel < 5)
                {
                    enemiesToSend.Add(enemiesLibrary[0]);
                }
                else if(player.GetComponent<Stats>().currentLevel > 4 && player.GetComponent<Stats>().currentLevel < 10)
                {
                    enemiesToSend.Add(enemiesLibrary[Random.Range(0, 2)]);
                }
                else if(player.GetComponent<Stats>().currentLevel > 10)
                {
                    enemiesToSend.Add(enemiesLibrary[Random.Range(0, 3)]);
                }*/
                switch (myType)
                {
                    case fieldType.spaghetti:
                        enemiesToSend.Add(enemiesLibrary[0]);
                        //add only gnocs to enemies to send
                        break;
                    case fieldType.broccoli:
                        enemiesToSend.Add(enemiesLibrary[Random.Range(0, 2)]);
                        //add only gnocs and ravaghoulis to enemies to send
                        break;
                    case fieldType.stew:
                        enemiesToSend.Add(enemiesLibrary[Random.Range(0, 3)]);
                        //add all enemies to enemies to send
                        break;
                }
            }
            gameManager.GetComponent<GameManager>().GenerateEnemies(enemiesToSend);
            //use game manager to send enemies to battlestage

            gameManager.GetComponent<GameManager>().TravelToWorld(GameManager.Worlds.BattleStage);
            //go to battlestage
        }
        StartCoroutine(CheckTimer());
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isInField = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isInField = false;
        }
    }
    IEnumerator CheckTimer()
    {
        yield return new WaitForSeconds(2);
        RollDice();
    }
}
