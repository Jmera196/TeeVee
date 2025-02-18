using UnityEngine;

public class ShurikenSpin : MonoBehaviour
{

    public Vector3 spinRotationSpeed = new Vector3(20, 0, 500);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
