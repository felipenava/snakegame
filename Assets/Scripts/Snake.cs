using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab; 
    public int initialSize;
    private Vector2 currentInput;

    // Start is called before the first frame update
    void Start()
    {
        ResetState();
    }

    // Update is called once per frame
    void Update()
    {
        // It means that the snake is moving weather to the right or to the left side.
        // So permit the snake only go up or down.
        if (this.direction.x != 0.0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentInput = Vector2.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentInput = Vector2.down;
            }
        // It means that the snake is moving weather to the right or to the left side.
        // So permit the snake only go left or right.
        } else if (this.direction.y != 0.0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentInput = Vector2.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentInput = Vector2.left;
            }
        }
    }

    void FixedUpdate()
    {
        // Update the direction based on current input from the user.
        if (currentInput != Vector2.zero)
        {
            direction = currentInput;
        }

        // the rest of the body movement.
        for(int i = segments.Count - 1; i > 0; --i)
        {
            segments[i].position = segments[i - 1].position;
        }

        // head movement.
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f
        );
    }

    private void ResetState()
    {
        for(int i = 1; i < segments.Count; ++i)
        {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(this.transform);

        for(int i = 1; i < this.initialSize; ++i)
        {
            segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero;
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segments[segments.Count - 1].position;

        segments.Add(segment);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Food")
        {
            Grow();
        } else if (other.tag == "Obstacle")
        {
            ResetState();
        }
    }

    public bool Occupies(float x, float y)
    {
        foreach(Transform segment in segments)
        {
            if (segment.position.x == x && segment.position.y == y)
            {
                return true;
            }
        }
        return false;
    }
}
