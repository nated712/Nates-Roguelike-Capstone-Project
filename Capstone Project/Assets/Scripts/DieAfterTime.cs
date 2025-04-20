using UnityEngine;

public class DieAfterTime : MonoBehaviour
{
    [SerializeField] float destroyAfterSeconds = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }


}
