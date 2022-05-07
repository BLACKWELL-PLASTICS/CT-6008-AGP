using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [HideInInspector]
    public FishScript spawner;

    private Transform child;
    private void Start()
    {
        child = GetComponentInChildren<Transform>();
    }

    void Update()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), spawner.fishSpeed * Time.deltaTime);
        child.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(transform.rotation.x, transform.rotation.y, -90, transform.rotation.w), Time.deltaTime * 0.005f);

        yield return new WaitForSeconds(spawner.fishJumpTime);

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 11, transform.position.z), (spawner.fishSpeed * 2) * Time.deltaTime);
        child.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(transform.rotation.x, transform.rotation.y, -90, transform.rotation.w), Time.deltaTime * 0.025f);

        yield return new WaitForSeconds(spawner.fishJumpTime + 0.2f);

        Destroy(this.gameObject);
    }
}
