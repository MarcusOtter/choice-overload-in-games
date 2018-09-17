using System;
using UnityEngine;

namespace Scripts
{
    /// <summary>Singleton class that tracks the user's input.</summary>
	public class UserInputController : MonoBehaviour
	{
	    internal static UserInputController Instance { get; private set; }

		internal event EventHandler OnAttackButtonDown = delegate { };
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

            // Invokes the OnAttackButtonDown event if the _attackKey is pressed and something is subscribed to the event.
            if (Input.GetKeyDown(_attackKey)) OnAttackButtonDown?.Invoke(this, EventArgs.Empty);

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

