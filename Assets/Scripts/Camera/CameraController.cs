using UnityEngine;

namespace UltimateCameraController.Cameras.Controllers
{
	[AddComponentMenu("Ultimate Camera Controller/Camera Controller")]
	public class CameraController : MonoBehaviour
	{
		[SerializeField] private Animator _playerAnimator;

		[Header("Follow Settings")]
		[Space(10)]
		
		[Tooltip("Should the camera follow the target?")]
		public bool followTargetPosition = true; 
		
		[Tooltip("The target object our camera should follow or orbit around")]
		public Transform targetObject; 

		[Tooltip("The smooth factor when the camera follows a target object")]
		[Range(0.2f, 1f)]
		public float cameraFollowSmoothness; 

		[Header("Orbit Settings")] 
		[Space(10)] 
		
		[Tooltip("Should the player be able to orbit around the target object?")]
		public bool orbitAroundTarget = true; 
		
		[Tooltip("The speed by which the camera rotates when orbiting")]
		[Range(2f, 15f)]
		public float rotationSpeed;

		[Tooltip("The mouse button that the player must hold in order to orbit the camera")]
		public MouseButtons mouseButton;
		
		private Vector3 _cameraOffset; 
		private float _angleRotate; 
		private float _speedRotate = 1f; 
		private bool _isRotate;

        private void Awake()
        {
			_angleRotate = transform.eulerAngles.x;
		}

        private void Start()
		{
			_cameraOffset = transform.position - targetObject.position;
		}

        private void Update()
        {
			if (_playerAnimator.GetBool("Jump") && !_isRotate)
            {
				_isRotate = true;
				_angleRotate += 20f;
			}
			else if (!_playerAnimator.GetBool("Jump") && _isRotate)
            {
				_isRotate = false;
				_angleRotate -= 20f;
			}		
		}

        private void FixedUpdate()
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(_angleRotate, 0, 0), Time.deltaTime * _speedRotate);

			if (targetObject == null)
			{
				Debug.LogError("Target Object is not assigned. Please assign a target object in the inspector.");
				return;
			}
			
			if (followTargetPosition)
			{
				var newPosition = targetObject.position + _cameraOffset;
				transform.position = Vector3.Slerp(transform.position, newPosition, cameraFollowSmoothness);
			}

			if (orbitAroundTarget)
			{
				OrbitCamera();
			}
		}

		private void OrbitCamera()
		{
			if (Input.GetMouseButton((int)mouseButton))
			{
				float y_rotate = Input.GetAxis("Mouse X") * rotationSpeed;
				float x_rotate = Input.GetAxis("Mouse Y") * rotationSpeed;
				
				Quaternion xAngle = Quaternion.AngleAxis(y_rotate, Vector3.up);
				Quaternion yAngle = Quaternion.AngleAxis(x_rotate, Vector3.left);

				_cameraOffset = xAngle * _cameraOffset;
				_cameraOffset = yAngle * _cameraOffset;

				transform.LookAt(targetObject);
			}
		}
	}

	public enum MouseButtons
	{
		LeftButton = 0,
		RightButton = 1,
		ScrollButton = 2
	};
}