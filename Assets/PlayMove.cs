using UnityEngine;
using System.Collections;

public class PlayMove : MonoBehaviour
{

    public int dist;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            this.transform.Translate(new Vector3(0, 1 * dist, 0));
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            this.transform.Translate(new Vector3(0, -1 * dist, 0));
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            this.transform.Translate(new Vector3(-1 * dist, 0, 0));
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            this.transform.Translate(new Vector3(1 * dist, 0, 0));
        }

    }

}
