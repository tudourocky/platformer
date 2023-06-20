using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    bool gameHasEnded = false;

    public void EndGameOne ()
    {
        if(gameHasEnded == false)
        {
            gameHasEnded = true;
            LoadPlayerOneWon();

        }
    }

    public void EndGameTwo()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;

            LoadPlayerTwoWon();
        }
    }

    public void LoadPlayerOneWon()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadPlayerTwoWon()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

}
