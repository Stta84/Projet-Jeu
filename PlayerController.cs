using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * 5f * Time.fixedDeltaTime * Input.GetAxis("Vertical"));
        transform.Translate(Vector3.right * 5f * Time.fixedDeltaTime * Input.GetAxis("Horizontal"));
    }
}
