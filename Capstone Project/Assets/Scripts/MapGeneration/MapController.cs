using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    //Store prefabs for the terrainchunks
    public List<GameObject> terrainChunks;
    //references the player
    public GameObject player;
    //checks the radius
    public float checkerRadius;
    //track which layer is the terrain and which is not the terrain
    public LayerMask terrainMask;
    //reference to access variables
    public GameObject currentChunk;
    Vector3 playerLastPosition;

    [Header("Optimization")]
    //stores the current chunks
    public List<GameObject> spawnedChunks;
    //last chunk that was spawned
    GameObject latestChunk;
    //Max optimization distance
    //used to set the max distance for each of the chunk from player
    public float maxOpDist; //Must be greater than the length and width of the tilemap
    //references the current distance for each chunks
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDur;

    // Start is called before the first frame update
    void Start()
    {
        playerLastPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        if (!currentChunk)
        {
            return;
        }

        Vector3 moveDir = player.transform.position - playerLastPosition;
        playerLastPosition = player.transform.position;

        string directionName = GetDirectionName(moveDir);

        CheckAndSpawnChunk(directionName);

        //Check additional adjacent directions for diagonal chunks
        if (directionName.Contains("Up"))
        {
            CheckAndSpawnChunk("Up");
            CheckAndSpawnChunk("Left Up");
            CheckAndSpawnChunk("Right Up");
            CheckAndSpawnChunk("Right");
            CheckAndSpawnChunk("Left");

        } else if (directionName.Contains("Down"))
        {
            CheckAndSpawnChunk("Down");
            CheckAndSpawnChunk("Down Left");
            CheckAndSpawnChunk("Down Right");
            CheckAndSpawnChunk("Right");
            CheckAndSpawnChunk("Left");
        } else if (directionName.Contains("Right"))
        {
            CheckAndSpawnChunk("Right");
            CheckAndSpawnChunk("Down Right");
            CheckAndSpawnChunk("Right Up");
            CheckAndSpawnChunk("Down");
            CheckAndSpawnChunk("Up");
        } else if (directionName.Contains("Left"))
        {
            CheckAndSpawnChunk("Left");
            CheckAndSpawnChunk("Down Left");
            CheckAndSpawnChunk("Left Up");
            CheckAndSpawnChunk("Up");
            CheckAndSpawnChunk("Down");
        }
    }

    void CheckAndSpawnChunk(string direction)
    {
        if(!Physics2D.OverlapCircle(currentChunk.transform.Find(direction).position, checkerRadius, terrainMask))
        {
            SpawnChunk(currentChunk.transform.Find(direction).position);
        }
    }

    string GetDirectionName(Vector3 direction)
    {
        direction = direction.normalized;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            //moving horizontally more than vertically
            if (direction.y > 0.5f)
            {
                //Also moving upwards
                return direction.x > 0 ? "Right Up" : "Left Up";
            }
            else if (direction.y < -0.5f)
            {
                //also moving downwards
                return direction.x > 0 ? "Down Right" : "Down Left";
            }
            else
            {
                //moving straight horizontally
                return direction.x > 0 ? "Right" : "Left";
            }
        }
        else
        {
            //moving vertically more than vertically
            if (direction.x > 0.5f)
            {
                //also moving right
                return direction.y > 0 ? "Right Up" : "Down Right";
            }
            else if (direction.x < -0.5f)
            {
                //also moving left
                return direction.y > 0 ? "Left Up" : "Down Left";
            }
            else
            {
                //moving straight vertically
                return direction.y > 0 ? "Up" : "Down";
            }
        }
    }

    void SpawnChunk(Vector3 spawnPosition)
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], spawnPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if(optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDur;
        }
        else
        {
            return;
        }

        //for loop
        foreach (GameObject chunk in spawnedChunks)
        {
            //distance of the chunk that is being checked
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            //checks if chunk dist is more than the max optimization dist and disable it if it is
            if (opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}