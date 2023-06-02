using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private Transform goal;
    [SerializeField] private float rotationSpeed = 10f;

    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, rotationSpeed * Time.deltaTime);
        var goalPosition = goal.position;
        Vector3 adjustedGoalPosition = new Vector3(goalPosition.x, goalPosition.y - 10f, goalPosition.z);
        Vector3 newLookAt = (adjustedGoalPosition + new Vector3(0, -10f, 0)) / 2f;
        transform.LookAt(adjustedGoalPosition);
    }
}
