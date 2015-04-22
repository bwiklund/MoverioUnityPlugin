using UnityEngine;
using System.Collections;

[AddComponentMenu("Moverio/HeadTrackingAR")]

public class HeadTrackingAR : MonoBehaviour {

    private static HeadTrackingAR _instance;
    public static HeadTrackingAR Instance {
        get {
            return _instance;
        }
    }

    bool gyroIsAvailable;

    void Awake() {
        _instance = this;
    }

    void Start() {
        gyroIsAvailable = SystemInfo.supportsGyroscope;

        if (SystemInfo.supportsGyroscope) {
            Gyroscope headGyro = Input.gyro;
            Input.gyro.enabled = true;
            Input.gyro.updateInterval = 1f / 60f;
            headGyro.updateInterval = 1f / 60f;
        }
    }

    void LateUpdate() {
        // TODO: there a some way to reduce this but quaternion multiplication is not commutative and am in a rush.
        Quaternion baseRotation = Quaternion.Euler(new Vector3(90, 180, 0));
        Quaternion callibration = Quaternion.Euler(new Vector3(0, 0, 180));

        if (gyroIsAvailable) {
            transform.localRotation = baseRotation * Input.gyro.attitude * callibration;//Quaternion.Slerp(transform.localRotation, Input.gyro.attitude * _callibration, Time.deltaTime*30);
        }
    }
}