using UnityEngine;
using UnityEngine.UI;

public class DropdownRoleChange : MonoBehaviour
{
    Dropdown m_Dropdown;
    public UnitHandler unitHandler;

    void Start()
    {
        //Fetch the Dropdown GameObject
        m_Dropdown = GetComponent<Dropdown>();
        //Add listener for when the value of the Dropdown changes, to take action
        m_Dropdown.onValueChanged.AddListener(delegate {
            DropdownValueChanged(m_Dropdown);
        });
    }

    //Ouput the new value of the Dropdown into Text
    void DropdownValueChanged(Dropdown change)
    {
        unitHandler.ChangeUnitRole((Unit.Role)change.value);
    }

    public void SetValue(int value)
    {
        GetComponent<Dropdown>().value = value;
    }
}