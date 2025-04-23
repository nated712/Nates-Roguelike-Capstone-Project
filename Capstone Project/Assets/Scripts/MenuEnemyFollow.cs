using UnityEngine;

public class MenuEnemyFolow : MonoBehaviour
{   
    Vector3 mousePosition;
    Vector2 targetPosition;
    public float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {   
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);
        FacePlayer();
        
    }

    void FacePlayer()
    {

        Vector2 direction = mousePosition - transform.position;
        transform.localScale = new Vector3(direction.x >= 0 ? -1 : 1, 1, 1);
    
    }
}
