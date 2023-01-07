using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class InsectMovement : MonoBehaviour
{
    public float MovementSpeed;
    private List<Vector2> _path;
    private int currentTarget = 0;
    private int layer = -1;

    // Start is called before the first frame update
    void Start()
    {
        _path = GameObject.Find("base tilemap").GetComponent<Path>().TilemapPath;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPosition = _path[currentTarget];
        Vector2 moveVector = Vector2.MoveTowards(transform.position, new Vector3(targetPosition.x, targetPosition.y, -1), MovementSpeed * Time.deltaTime);

        float angle = Mathf.Atan2(moveVector.y - transform.position.y , moveVector.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        transform.position = new Vector3(moveVector.x, moveVector.y, layer);

        if ((Vector2)transform.position == _path[currentTarget])
        {
            currentTarget++;
        }

        if (currentTarget == _path.Count)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger enter in insect");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("collision enter in insect");
    }
}
