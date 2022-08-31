using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UnitHandler : MonoBehaviour
{

    public List<Unit> unitList = new List<Unit>();
   
    public List<GameObject> prefabs = new List<GameObject>();

    private void Awake()
    {
        
        SpawnUnits(9);
        
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnUnits(int numberOfSpawns)
    {
        for (int i = 0; i < numberOfSpawns; i++)
        {
        Transform spawn = GameObject.Instantiate(prefabs[0]).GetComponent<Transform>();
            spawn.position = GetComponent<PositionHandler>().mPositions[i];
            unitList.Add(spawn.GetComponent<Unit>());
        }
    }
}
