using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;
    private Snake snake;

    private void Awake()
    {
        snake = FindObjectOfType<Snake>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        RandomizePosition();
    }

    private void RandomizePosition()
    {
        Vector3 foodPosition = SortPosition();

        // While the food occupies the same sanek position, sort new food position.
        while(snake.Occupies(foodPosition.x, foodPosition.y))
        {
            foodPosition = SortPosition();
        }

        // Assign the final position
        this.transform.position = foodPosition;
    }

    private Vector3 SortPosition()
    {
        Bounds bound = this.gridArea.bounds;

        // Pick a random position inside the bounds
        float x = Random.Range(bound.min.x, bound.max.x);
        float y = Random.Range(bound.min.y, bound.max.y);

        // Round the values to ensure it aligns with the grid
        x = Mathf.Round(x);
        y = Mathf.Round(y);

        return new Vector3(x, y, 0.0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            RandomizePosition();
        }
    }
}
