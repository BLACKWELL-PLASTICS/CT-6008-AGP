using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [HideInInspector]
    public FishScript spawner;
    void Update()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), AIManager.GetFishSpeed * Time.deltaTime);

        yield return new WaitForSeconds(AIManager.GetFishTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, new Quaternion(180, 0, 0, 0), Time.deltaTime * 0.03f);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 11, transform.position.z), (AIManager.GetFishSpeed * 2) * Time.deltaTime);

        yield return new WaitForSeconds(AIManager.GetFishTime + 0.2f);

        Destroy(this.gameObject);
    }
}
