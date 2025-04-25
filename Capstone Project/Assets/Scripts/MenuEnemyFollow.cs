using UnityEngine;

public class MenuEnemyFolow : MonoBehaviour
{   
    Vector3 mousePosition;
    Vector2 targetPosition;
    Vector2 previousPosition;
    public float speed = 5f;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {   
        previousPosition = transform.position;
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = Vector2.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);
        float movement = ((Vector2)transform.position - previousPosition).magnitude / Time.deltaTime;
        animator.SetFloat("Speed", movement);
        FacePlayer();
        
    }

    void FacePlayer()
    {

        Vector2 direction = mousePosition - transform.position;
        transform.localScale = new Vector3(direction.x >= 0 ? -1 : 1, 1, 1);
    
    }
}
