using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public bool disableInput;
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		public bool pickUp;

		public bool interact;
    public bool anyKeyPressed;

		[Header("Movement Settings")]
		public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = false;
		public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			if (disableInput) return;
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (disableInput) return;
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			if (disableInput) return;
			Debug.Log("On Jump: " + value.isPressed);
			// JumpInput(value.isPressed);//no jump!
		}


		public void OnSprint(InputValue value)
		{
			if (disableInput) return;
			Debug.Log("On Sprint: " + value.isPressed);
			SprintInput(value.isPressed);
		}

		public void OnPickUp(InputValue value)
		{
			if (disableInput) return;
			// Debug.Log("On Pickup: " + value.isPressed);
			PickUpInput(value.isPressed);
		}

		public void OnInteract(InputValue value)
		{
			if (disableInput) return;
			// Debug.Log("On Interact: " + value.isPressed);
			InteractInput(value.isPressed);
		}

    public void OnAnyKey(InputValue value)
    {
		if (disableInput) return;
      Debug.Log("On Anykey: " + value.isPressed);
      AnyKeyInput(value.isPressed);
    }
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void PickUpInput(bool newPickUpState)
		{
			pickUp = newPickUpState;
		}

		public void InteractInput(bool newInteractState)
		{
			interact = newInteractState;
		}

    public void AnyKeyInput(bool newAnyKeyState)
    {
      anyKeyPressed = newAnyKeyState;
    }

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			//SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			//Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}

}
