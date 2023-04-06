using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSC : MonoBehaviour
{

    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayerSumon()
    {
        Instantiate(Player, transform.position,transform.rotation);//ƒƒCƒ„[‚Ìæ’[•”‚Ì¶¬‚ÆŠi”[
    }
}
