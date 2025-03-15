using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PManager>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyData.MoveSpeed * Time.deltaTime); //Constant movement towards player

        FacePlayer();
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
}
