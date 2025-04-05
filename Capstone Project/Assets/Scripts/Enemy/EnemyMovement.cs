using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    Transform player;
    bool pause = false;

    // Start is called before the first frame update
    void Start()
    {
        player = FindFirstObjectByType<PManager>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FacePlayer();
    }

    void Move()
    {
        if(pause == false){
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyData.MoveSpeed * Time.deltaTime); //Constant movement towards player
        }
    }
    void FacePlayer(){
        if (player != null){
            Vector2 direction = player.position - transform.position;
                        // Flip based on the horizontal direction
            if (direction.x >= 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Facing left
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1); // Facing right
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            other.GetComponent<HealthManaManager>().takeDamage(20);
        }
        stopMoving();

    }

    IEnumerator stopMoving(){
        pause = true;
        yield return new WaitForSeconds(2);
        pause = false;

    }


}
