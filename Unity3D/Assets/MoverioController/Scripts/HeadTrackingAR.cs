using UnityEngine;
using System.Collections;

[AddComponentMenu("Moverio/HeadTrackingAR")]

public class HeadTrackingAR : MonoBehaviour
{

	private static HeadTrackingAR _instance;
	public static HeadTrackingAR Instance
	{
		get
		{
			return _instance;
		}
	}

	bool _gyroIsAvailable;
	Gyroscope _head;
	Quaternion _callibration;
	Transform _t;
	
	float yO = 0.0f;

	void Awake()
	{
		_instance = this;
	}

	
	void Start()
	{
		_t = transform;

		yO = _t.position.y;

		_gyroIsAvailable = SystemInfo.supportsGyroscope;
		
		if (_gyroIsAvailable) 
		{		
			_head = Input.gyro;
			Input.gyro.enabled = true;
			Input.gyro.updateInterval = 1f/60f;
			_head.updateInterval = 1f/60f;
		}
	}

	
	void Update()
	{
		transform.eulerAngles = new Vector3(90,180,0);
		_callibration = Quaternion.Euler(new Vector3(0,0,180));
		
		if (_gyroIsAvailable) 
		{
			_t.localRotation = Input.gyro.attitude * _callibration;//Quaternion.Slerp(transform.localRotation, Input.gyro.attitude * _callibration, Time.deltaTime*30);
		}
	}
}