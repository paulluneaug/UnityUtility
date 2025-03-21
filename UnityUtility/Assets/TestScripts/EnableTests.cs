using UnityEngine;

public class EnableTests : MonoBehaviour
{

    private void OnEnable()
    {
        Debug.Log("EnableTests.OnEnable");
    }

    private void Awake()
    {
        Debug.Log("EnableTests.Awake");
    }
    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("EnableTests.Start");
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log("EnableTests.Update");
    }

    private void FixedUpdate()
    {
        Debug.Log("EnableTests.FixedUpdate");
    }

    private void LateUpdate()
    {
        Debug.Log("EnableTests.LateUpdate");
    }

    private void OnApplicationQuit()
    {
        Debug.Log("EnableTests.OnApplicationQuit");
    }

    private void OnDisable()
    {
        Debug.Log("EnableTests.OnDisable");
    }

    private void OnApplicationPause(bool pause)
    {
        Debug.Log("EnableTests.OnApplictionPause");
    }

    private void OnDestroy()
    {
        Debug.Log("EnableTests.OnDestroy");
    }

    private void OnDrawGizmos()
    {
        Debug.Log("EnableTests.OnDrawGizmos");
    }
}
