using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 target;
    float speed = 7.5f;
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (this.transform.position == target)
            Destroy(gameObject);
    }

    public void SetTarget(int x, int y)
    {
        target = new Vector3(x, 0, y);
    }
}
