using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PositionHandler : MonoBehaviour
{

    public List<Vector3> mPositions = new List<Vector3>();
    [Range(1, 10)]
    public int spread = 1;
    private GameObject positions;

    public bool locationsMade = false;

    private void Awake()
    {
        DisplayGrid();
        locationsMade = true;
        
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DisplayGrid()
    {

        positions = new GameObject("Positions");
        for (int i = 0; i < 3; i++)
        {

            for (int j = 0; j < 3; j++)
            {
                GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Destroy(gameObject.GetComponent<SphereCollider>());
                gameObject.transform.SetParent(positions.transform, false);
                mPositions.Add(new Vector3(-1 * spread, 1, -1 * spread) + new Vector3(j * spread, 0, i * spread));
                gameObject.transform.position = mPositions[i + j];

            }

        }
    }

    public void UpdateGrid()
    {
        foreach (Transform position in positions.transform.GetComponentsInChildren<Transform>())
        {
            position.position = new Vector3(-1, 0, -1) + new Vector3();
        }
    }






}
