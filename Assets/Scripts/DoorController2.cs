using UnityEngine;

public class DoorController2 : MonoBehaviour
{
    public Animator doorAnim2;

    private bool doorOpen = false;

    private void Update()
    {
        doorAnim2 = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation2()
    {
        if(!doorOpen)
        {
            doorAnim2.Play("DoorOpenL", 0, 0.0f);
            doorOpen = true;

        }
        else
        {
            doorAnim2.Play("DoorCloseL", 0, 0.0f);
            doorOpen = false;
        }
    }
}
