using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class MechManager : MonoBehaviour
{
    public GameObject mech01;
    public GameObject mech02;
    public GameObject mech03;

    public Transform spawn01;
    public Transform spawn02;
    public Transform spawn03;

    private Transform player;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void SpawnMech01()
    {
        AIDestinationSetter mech = Instantiate(mech01, spawn01.position, Quaternion.identity)?.GetComponent<AIDestinationSetter>();
        mech.transform.position= new Vector3(mech.transform.position.x, mech.transform.position.y, 0);
        mech.target = player;

    }    public void SpawnMech02()
    {
        AIDestinationSetter mech = Instantiate(mech02, spawn02.position, Quaternion.identity)?.GetComponent<AIDestinationSetter>();
        mech.target = player;
        mech.transform.position = new Vector3(mech.transform.position.x, mech.transform.position.y, 0);
    }
    public void SpawnMech03()
    {
        AIDestinationSetter mech = Instantiate(mech03, spawn03.position, Quaternion.identity)?.GetComponent<AIDestinationSetter>();
        mech.target = player;
        mech.transform.position = new Vector3(mech.transform.position.x, mech.transform.position.y, 0);
    }
}
