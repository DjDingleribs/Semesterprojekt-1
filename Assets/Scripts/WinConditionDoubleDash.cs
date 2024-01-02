using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinConditionDoubleDash : MonoBehaviour
{
    public Player1Movement pl1;

    // Update is called once per frame
    void Update()
    {
        if (pl1.player1GoalAchieved == true)
        {
            Debug.Log("Win");
            //winCon();
        }
    }

    private void winCon()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
