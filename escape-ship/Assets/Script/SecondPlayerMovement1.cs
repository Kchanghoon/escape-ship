using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayerMovement : MonoBehaviour
{
    void Update() { 
	if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(Vector3.forward* Time.deltaTime*3); //앞으로 이동
        }
if (Input.GetKey(KeyCode.DownArrow))
    {
    this.transform.Translate(Vector3.back * Time.deltaTime * 3); //뒤로 이동
    }
if (Input.GetKey(KeyCode.RightArrow))
    {
    this.transform.Translate(Vector3.right * Time.deltaTime * 3); //오른쪽으로 이동
    }
if (Input.GetKey(KeyCode.LeftArrow))
    {
    this.transform.Translate(Vector3.left * Time.deltaTime * 3); //왼쪽으로 이동
    }
    }
}