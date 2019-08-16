using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public float followSpeed;
    public Transform followLocation;


    private void Start()
    {
        GameObject gameMan = GameObject.FindGameObjectWithTag("GameManager");
        gameMan.GetComponent<GameManager>().LoadPlayerStuff(true);
    }

    void Update()
    {
        float t = followSpeed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, followLocation.position, t);
    }
}