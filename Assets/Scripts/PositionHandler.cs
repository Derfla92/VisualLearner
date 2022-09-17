using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PositionHandler : MonoBehaviour
{

    public List<Vector3> mPositions = new List<Vector3>();
    private GameObject positions;
    public bool showPositions = false;

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

    public void SquareGrid()
    {
        if (GameObject.Find("Positions"))
        {
            Destroy(GameObject.Find("Positions"));
        }
        mPositions.Clear();
        positions = new GameObject("Positions");
        for (int i = 0; i < 5; i++)
        {

            for (int j = 0; j < 5; j++)
            {

                    GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    Destroy(gameObject.GetComponent<SphereCollider>());
                    gameObject.transform.SetParent(positions.transform, false);
                    gameObject.transform.position = new Vector3(-1 * 6f, 5, -1 * 1f) + new Vector3(j * 3, 0, i * -3);

                mPositions.Add(gameObject.transform.position);
                



            }
        }
        if (!showPositions) Destroy(positions);
    }

    public void FanGrid()
    {
        if (GameObject.Find("Positions"))
        {
            Destroy(GameObject.Find("Positions"));
        }
        mPositions.Clear();
        positions = new GameObject("Positions");
        int row = 0;
        int x = 0;
        for (int i = 1; i <= 25; i++)
        {
            float angle;
            if (i == 1)
            {
                angle = 0;
            }
            else
            {
                angle = 45 - 90 / (row) * x;
            }
            Vector3 startPos = Vector3.up * 5;
            
                GameObject newSpawn = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                newSpawn.transform.position = startPos;
                Destroy(newSpawn.GetComponent<SphereCollider>());
                newSpawn.transform.SetParent(positions.transform, false);
                newSpawn.transform.Rotate(Quaternion.Euler(0, angle, 0).eulerAngles);
                newSpawn.transform.Translate(new Vector3(0, 0, -3 * (row)), Space.Self);
            

            mPositions.Add(newSpawn.transform.position);

            x++;

            if (x == row + 1)
            {
                row++;
                x = 0;
            }
        }
        if (!showPositions) Destroy(positions);

    }

    public GameObject CreateVisualSpawnPoint(int x, int y)
    {
        GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Destroy(gameObject.GetComponent<SphereCollider>());
        gameObject.transform.SetParent(positions.transform, false);
        mPositions.Add(new Vector3(-1 * 2f, 1, -1 * 2f) + new Vector3(x * 5, 0, y * 5));
        gameObject.transform.position = mPositions[x + y];
        return gameObject;
    }

    public void UpdateGrid()
    {
        foreach (Transform position in positions.transform.GetComponentsInChildren<Transform>())
        {
            position.position = new Vector3(-1, 0, -1) + new Vector3();
        }
    }






}
