using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int score;
    public Vector3 position;

    void Update()
    {
        // �� �����Ӹ��� ���� ��ġ�� ������ ������Ʈ
        position = transform.position;
        score = GetComponent<Player>().score; // ���÷� ������ �������� �κ�
    }


    public GameState GetGameState()
    {
        GameState gameState = new GameState();
        gameState.playerScore = score;
        gameState.playerPositionX = transform.position.x;
        gameState.playerPositionY = transform.position.y;
        gameState.playerPositionZ = transform.position.z;

        return gameState;
    }

    public void SetGameState(GameState gameState)
    {
        score = gameState.playerScore;
        transform.position = new Vector3(gameState.playerPositionX, gameState.playerPositionY, gameState.playerPositionZ);
    }
}
