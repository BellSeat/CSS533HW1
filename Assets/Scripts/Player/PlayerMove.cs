using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    enum Direction { Up, Down, Left, Right };
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //int direction = getDirection();
        //if (direction != -1)
        //{
        //    basicaMove(direction);
        //}
    }

    //void basicaMove(int direction)
    //{
    //    switch (direction)
    //    {
    //        case (int)Direction.Up:
    //            transform.position += new Vector3(0, 1, 0) * speed;
    //            break;
    //        case (int)Direction.Down:
    //            transform.position += new Vector3(0, -1, 0) * speed;
    //            break;
    //        case (int)Direction.Left:
    //            transform.position += new Vector3(-1, 0, 0) * speed;
    //            break;
    //        case (int)Direction.Right:
    //            transform.position += new Vector3(1, 0, 0) * speed;
    //            break;
    //        default:
    //            break;
    //    }
    //}

    //int getDirection()
    //{
    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        return (int)Direction.Up;
    //    }
    //    else if (Input.GetKey(KeyCode.S))
    //    {
    //        return (int)Direction.Down;
    //    }
    //    else if (Input.GetKey(KeyCode.A))
    //    {
    //        return (int)Direction.Left;
    //    }
    //    else if (Input.GetKey(KeyCode.D))
    //    {
    //        return (int)Direction.Right;
    //    }
    //    return -1;
    //}

    void setPlayerPosition(Vector2 position)
    {

        transform.position = new Vector3(position.x, position.y, 0);
    }

    void setPlayerRotation(Quaternion rotation) { }
}
