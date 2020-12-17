using UnityEngine;

/**
 * This component allows the player to move by clicking the arrow keys.
 */
public class KeyboardMover : MonoBehaviour {
    public bool twoButt = false;

    //moving the player and checking if he presses on the 'x' button simultanly
    protected Vector3 NewPosition() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if (Input.GetKey("x")){
                twoButt = true;
            }
            return transform.position + Vector3.left;
        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (Input.GetKey("x")){
                twoButt = true;
            }
            return transform.position + Vector3.right;
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if (Input.GetKey("x")){
                twoButt = true;
            }
            return transform.position + Vector3.down;
        } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (Input.GetKey("x")){
                twoButt = true;
            }
            return transform.position + Vector3.up;
        } else {
            return transform.position;
        }
    }


    void Update()  {
        transform.position = NewPosition();
    }
}
