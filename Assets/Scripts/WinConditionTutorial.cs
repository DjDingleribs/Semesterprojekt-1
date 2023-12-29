using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinConditionTutorial : MonoBehaviour
{
    public Player1Movement pl1;
    public Player2Movement pl2;

    // Update is called once per frame
    void Update()
    {
        if (pl1.player1GoalAchieved == true && pl2.player2GoalAchieved == true)
        {
            winCon();
        }
    }

    private void winCon()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
