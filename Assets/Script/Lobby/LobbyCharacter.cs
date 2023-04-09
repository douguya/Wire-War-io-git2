using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCharacter : MonoBehaviour
{
    public float Rotation_Speed;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,Rotation_Speed,0);


       
    }
}
