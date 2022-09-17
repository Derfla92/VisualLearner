using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class UnitHandler : MonoBehaviour
{

    public List<Unit> unitList = new List<Unit>();

    public GameObject tankPrefab;
    public GameObject healerPrefab;
    public GameObject damageDealerPrefab;

    public int tanks;
    public int healers;
    public int damageDealers;

    public Text infoPane;
    public DropdownRoleChange dropdown;
    public Unit selectedUnit;

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
            if(!unit.TryGetComponent<Boss>(out Boss boss))
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
            spawn.parent = GameObject.Find("Heroes").transform;
            unitList.Add(spawn.GetComponent<Unit>());

        }
    }

    public void UpdateInfoTab()
    {
        infoPane.text = "Name: " + selectedUnit.name + "\n" +
                        "Role: " + selectedUnit.role + "\n" +
                        "Health: " + selectedUnit.hitPoints + "\n" +
                        "Damage: " + selectedUnit.attackDamage;
        if (selectedUnit.role != Unit.Role.Boss)
        {
            dropdown.transform.gameObject.SetActive(true);
            dropdown.SetValue((int)selectedUnit.role);
        }
        else
        {
            dropdown.transform.gameObject.SetActive(false);
        }
    }

    public void ChangeUnitRole(Unit.Role role)
    {

        if (selectedUnit.role != role)
        {
            Unit replacement;
            switch (role)
            {
                case Unit.Role.Healer:
                    replacement = Instantiate(healerPrefab).GetComponent<Unit>();
                    replacement.transform.position = selectedUnit.transform.position;
                    replacement.transform.rotation = selectedUnit.transform.rotation;
                    Destroy(selectedUnit.gameObject);
                    selectedUnit = replacement;
                    break;
                case Unit.Role.Tank:
                    replacement = Instantiate(tankPrefab).GetComponent<Unit>();
                    replacement.transform.position = selectedUnit.transform.position;
                    replacement.transform.rotation = selectedUnit.transform.rotation;
                    Destroy(selectedUnit.gameObject);
                    selectedUnit = replacement;
                    break;
                case Unit.Role.DamageDealer:
                    replacement = Instantiate(damageDealerPrefab).GetComponent<Unit>();
                    replacement.transform.position = selectedUnit.transform.position;
                    replacement.transform.rotation = selectedUnit.transform.rotation;
                    Destroy(selectedUnit.gameObject);
                    selectedUnit = replacement;
                    break;

                default:
                    break;
            }
            UpdateInfoTab();
            
        }
    }


}
