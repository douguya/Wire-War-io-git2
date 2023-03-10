using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    public int Bullet_Speed;//’e‘¬

    public int Ballet_Range;//ŽË’ö‹——£

    public Vector3 StartPosi;//”­ŽËŽž‚ÌˆÊ’u
    // Start is called before the first frame update
    void Start()
    {
        StartPosi = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.right * Time.deltaTime * Bullet_Speed);

        if (Vector3.Distance(transform.position, StartPosi)> Ballet_Range)
        {
            Destroy(this.gameObject);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }


}
