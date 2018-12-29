using System;
using UnityEngine;

namespace Scripts.Game
{
    /// <summary>Singleton class that tracks the user's input.</summary>
	public class UserInput : MonoBehaviour
	{
	    internal static UserInput Instance { get; private set; }

	    internal event EventHandler OnAttackKeyDown;
	    internal event EventHandler OnAttackKeyUp;

        internal float HorizontalAxis { get; private set; }
	    internal float VerticalAxis { get; private set; }
        internal Vector3 MouseWorldPosition { get; private set; }

		[SerializeField] private KeyCode _attackKey = KeyCode.Mouse0;
	    [SerializeField] [Tooltip("Defined in Unity's InputManager")] private string _horizontalAxisName = "Horizontal";
	    [SerializeField] [Tooltip("Defined in Unity's InputManager")] private string _verticalAxisName = "Vertical";

        private Camera _mainCamera;

	    private void Awake()
	    {
            SingletonCheck();
	        _mainCamera = Camera.main;
	    }

	    private void Update()
	    {
	        MouseWorldPosition = GetMouseWorldPosition();

	        if (Input.GetKeyDown(_attackKey))
	        {
	            OnAttackKeyDown?.Invoke(this, EventArgs.Empty);
            }

	        if (Input.GetKeyUp(_attackKey))
	        {
                OnAttackKeyUp?.Invoke(this, EventArgs.Empty);
            }

	        HorizontalAxis = Input.GetAxis(_horizontalAxisName);
	        VerticalAxis = Input.GetAxis(_verticalAxisName);
	    }

	    private Vector3 GetMouseWorldPosition()
	    {
	        if (_mainCamera == null)
	        {
	            _mainCamera = Camera.main;
	        }

	        return _mainCamera.ScreenToWorldPoint(Input.mousePosition);
	    }

        /// <summary>Simple check that assures that this is the only object of this type. Destroys duplicate objects.</summary>
	    private void SingletonCheck()
	    {
	        if (Instance != null)
	        {
	            Destroy(gameObject);
	            return;
	        }

	        Instance = this;
	        DontDestroyOnLoad(gameObject);
	    }
    }
}

