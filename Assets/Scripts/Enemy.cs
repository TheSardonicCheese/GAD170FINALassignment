using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour

{
    private GameObject battleManager;

    public Stats myStats;

    public enum EnemyTypes
    {
        gnoc,
        raveghouli,
        monsterella,
    }

    public EnemyTypes myType;



	void Start () 
    {
        battleManager = GameObject.FindGameObjectWithTag("BattleManager");

		myStats = GetComponent<Stats>();

        switch (myType)
        {
            case EnemyTypes.gnoc:
                //do setup
                break;
            case EnemyTypes.raveghouli:
                //do thing
                break;
            case EnemyTypes.monsterella:
                //do thing
                break;
        }
	}

    /*private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GetComponent<GameManager>().TravelToWorld(GameManager.Worlds.BattleStage);
            Destroy(col.gameObject);
        }
    }*/

    public void Defeated()
    {
        //removing enemies from list
        battleManager.GetComponent<BattleManager>().RemoveEnemy(gameObject);
    }
}
