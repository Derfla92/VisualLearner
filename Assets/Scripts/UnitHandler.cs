using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UnitHandler : MonoBehaviour
{

    public List<Unit> unitList = new List<Unit>();
   
    public List<GameObject> prefabs = new List<GameObject>();

    public int numberOfHeroes;

    private void Awake()
    {
        
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnUnits(numberOfHeroes);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnUnits(int numberOfSpawns)
    {
        for (int i = 0; i < numberOfSpawns; i++)
        {
        Transform spawn = GameObject.Instantiate(prefabs[0]).GetComponent<Transform>();
            spawn.position = GetComponent<PositionHandler>().mPositions[i];
            unitList.Add(spawn.GetComponent<Unit>());
        }
    }

    
}
