//////////////////////////////////////////////////
/// Created:                                   ///
/// Author:                                    ///
/// Edited By: Iain Farlow                     ///
/// Last Edited: 29/04/2022                    ///
//////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumScript : MonoBehaviour
{
    float timer = 0f;
    int counter = 0;
    [SerializeField] int MAXHITCOUNTER;

    private void Update() {
        timer += Time.deltaTime;
        if (counter == 1) {
            gameObject.transform.localScale = gameObject.transform.localScale / 2;
        }
        if (timer >= 30f  || counter > MAXHITCOUNTER) {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        //if (other.gameObject.tag == "Terrain") { return; } Old

        //Added by Iain
        if (other.gameObject.tag != "Player") { return; } 
        if (other.gameObject.GetComponent<Controller>() != null)
        {
            other.gameObject.GetComponent<Controller>().isStuck = true;
        }
        else
        {
            other.gameObject.AddComponent<GumOnlineVisualTrigger>();
            if(other.gameObject.GetComponent<AIPlayer>() != null)
            {
                other.gameObject.GetComponent<PlayerHit>().HitSpin();
            }
        }
        //Added by Iain ~

        other.gameObject.transform.Find("Smoke").GetComponent<ParticleSystem>().Play();
        counter++;
    }

}
