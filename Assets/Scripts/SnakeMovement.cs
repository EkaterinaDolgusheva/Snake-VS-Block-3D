using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    public float Speed;
    public GameController Game;
    public Transform SnakeHead;
    public int value;
    public int Health = 1;
    private Rigidbody Rigidbody;
    Vector3 tempVect = new Vector3(0, 0, 1);
    private Vector3 _previousMousePosition;
    public TextMeshPro PointsText;
    public int Length = 1;
    private HitBoxBehavior componentSnakeTail;
    public float Sensitivity;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        PointsText.SetText(Health.ToString());
        componentSnakeTail = GetComponent<HitBoxBehavior>();
    }

    void FixedUpdate()
    {
        tempVect = Speed * Time.deltaTime * tempVect.normalized;
        Vector3 newPosition = transform.position + tempVect;

        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - _previousMousePosition;
            delta = Speed * Time.deltaTime * delta.normalized;
            newPosition.x += delta.x * Sensitivity;
        }

        Rigidbody.MovePosition(newPosition);
        _previousMousePosition = Input.mousePosition;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            value = collision.gameObject.GetComponent<FoodBehavior>().Value;
            Health += value;
            PointsText.SetText(Health.ToString());
            Destroy(collision.gameObject);

            for (int i = 0; i < value; i++)
            {
                Length++;
                componentSnakeTail.AddCircle();
            }
        }
        else if (collision.gameObject.tag == "Block")
        {
            value = collision.gameObject.GetComponent<AutoDestroy>().Value;
            if (value >= Health)
            {
                Game.OnPlayerDied();
                Rigidbody.velocity = Vector3.zero;
            }
            else
            {
                Health -= value;
                PointsText.SetText(Health.ToString());
                Destroy(collision.gameObject);

                for (int i = 0; i < value; i++)
                {
                    Length--;
                    componentSnakeTail.RemoveCircle();
                }
            }
        }
        else if (collision.gameObject.tag == "Finish")
        {
            Game.OnPlayerWon();
        }
    }
}
