using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UnitHandler : MonoBehaviour
{

    public List<Unit> unitList = new List<Unit>();

    public GameObject tankPrefab;
    public GameObject healerPrefab;
    public GameObject damageDealerPrefab;

    public int tanks;
    public int healers;
    public int damageDealers;

    private void Awake()
    {



    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnUnits()
    {
        foreach (Unit unit in unitList)
        {
            Destroy(unit.gameObject);
        }
        unitList.Clear();
        int total = tanks + healers + damageDealers;
        for (int i = 0; i < total; i++)
        {
            Transform spawn;
            if (i < tanks)
            {
                spawn = GameObject.Instantiate(tankPrefab).GetComponent<Transform>();
            }
            else if (i >= tanks && i < tanks + healers)
            {
                spawn = GameObject.Instantiate(healerPrefab).GetComponent<Transform>();
            }
            else
            {
                spawn = GameObject.Instantiate(damageDealerPrefab).GetComponent<Transform>();
            }
            spawn.position = GetComponent<PositionHandler>().mPositions[i];
            unitList.Add(spawn.GetComponent<Unit>());
        }
    }


}
