using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lap : MonoBehaviour
{
    [SerializeField]
    private bool isDrivenThrough;

    public bool IsDrivenThroughGetter() {
        return isDrivenThrough;
    }
    public void IsDrivenThroughSetter() {
        isDrivenThrough = !isDrivenThrough;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            isDrivenThrough = !isDrivenThrough;
        }
    }
}
