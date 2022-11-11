using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public SnakeMovement Controls;
    public GameObject Loss;
    public static State gameState;

    public enum State
    {
        Playing,
        Won,
        Loss,
    }

    public State CurrentState { get; private set; }

    public void OnPlayerDied()
    {
        if (CurrentState != State.Playing) return;
        CurrentState = State.Loss;
        Controls.enabled = false;
        Debug.Log("Game Over!");
        Loss.SetActive(true);
    }

    public void OnPlayerWon()
    {
        if (CurrentState != State.Playing) return;
        CurrentState = State.Won;
        Controls.enabled = false;
        Debug.Log("You Won!");
    }
}
