using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int score;
    public Vector3 position;

    void Update()
    {
        // 매 프레임마다 현재 위치와 점수를 업데이트
        position = transform.position;
        score = GetComponent<Player>().score; // 예시로 점수를 가져오는 부분
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
