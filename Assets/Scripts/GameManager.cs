using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject summaryPanel;
    public UnitHandler unitHandler;
    public TimeManager timeManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver()
    {
        if (unitHandler.unitList.Count < 1)
        {
            Debug.Log("Game Over!");
            summaryPanel.SetActive(true);
            summaryPanel.GetComponentInChildren<Text>().text = "Time: " + ((int)timeManager.currentTime).ToString();
            timeManager.StopTime();
        }
    }

    public void ResetRound()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        summaryPanel.SetActive(false);
    }


}
