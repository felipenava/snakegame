using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;
    // Start is called before the first frame update
    private void Start()
    {
        RandomizePosition();
    }

    private void RandomizePosition()
    {
        Bounds bound = this.gridArea.bounds;
        this.transform.position = new Vector3(
             Mathf.Round(Random.Range(bound.min.x, bound.max.x)),
             Mathf.Round(Random.Range(bound.min.y, bound.max.y)),
             0.0f
        ); 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            RandomizePosition();
        }
    }
}
