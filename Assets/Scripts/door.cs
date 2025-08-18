using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class door : MonoBehaviour
{
   public float interactionDistance;
   public GameObject intText;
   public string doorOpenAnimName, doorCloseAnimName;

   void Update(){
    Ray ray = new Ray(transform.position, transform.forward);
    RaycastHit hit;

    if(Physics.Raycast(ray, out hit, interactionDistance))
    {
        if(hit.collider.tag == "closetDoorL")
        {

          if(hit.collider.tag == "closetDoorL")
          {
          Debug.Log("Hit object with tag: " + hit.collider.tag);
          // ... sisa kode ...
          } else {
            Debug.Log("Object is not tagged as 'closetDoorL'");
          }

          GameObject grpDoorL = hit.collider.transform.root.gameObject;
          Animator closetAnim = grpDoorL.GetComponent<Animator>();
          intText.SetActive(true);
          if(Input.GetKeyDown(KeyCode.E))
          {
            if(closetAnim.GetCurrentAnimatorStateInfo(0).IsName(doorOpenAnimName))
            {
                closetAnim.ResetTrigger("DoorOpen");
                closetAnim.SetTrigger("DoorClose");
            }

              if(closetAnim.GetCurrentAnimatorStateInfo(0).IsName(doorCloseAnimName))
            {
                closetAnim.ResetTrigger("DoorClose");
                closetAnim.SetTrigger("DoorOpen");
            }
          }
        }
        else
        {
            intText.SetActive(false);
        }
    }
   }
}
