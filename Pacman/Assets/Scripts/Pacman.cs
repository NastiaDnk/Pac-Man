using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movements))]
public class Pacman : MonoBehaviour
{
    public Movements movement { get; private set; }

    private void Awake()
    {
        movement = GetComponent<Movements>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            movement.SetDirection(Vector2.up);
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            movement.SetDirection(Vector2.left);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            movement.SetDirection(Vector2.down);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            movement.SetDirection(Vector2.right);

        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }
    
}
