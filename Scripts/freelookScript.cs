using UnityEngine;
using UnityEngine.InputSystem;

public class freelookScript : MonoBehaviour
{
    public InputAction lookAction;
    public Transform target;
    public static float offsetAmount = 4f;

    void Update()
    {
        // Left
        if (Input.GetKey(KeyCode.L))
        {
            target.localPosition = new Vector2 (offsetAmount, 0);
        }
        // Down
        if (Input.GetKey(KeyCode.K))
        {
            target.localPosition = new Vector2 (0, -offsetAmount);
        }
        // Left
        if (Input.GetKey(KeyCode.J))
        {
            target.localPosition = new Vector2 (-offsetAmount, 0);
        }
        // Up
        if (Input.GetKey(KeyCode.I))
        {
            target.localPosition = new Vector2 (0, offsetAmount);
        }
        // Reset
        if (Input.GetKey(KeyCode.O))
        {
            target.localPosition = new Vector2 (0, 0);
        }
    }
}
