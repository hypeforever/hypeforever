using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [Serializable]
    public class MouseLook 
    {
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;
        public bool clampVerticalRotation = true;
        public float MinimumX = -90F;
        public float MaximumX = 90F;
        public bool smooth;
        public float smoothTime = 5f;
        public bool lockCursor = true;
        private SceneController m_SceneController;
        private GameObject m_SceneControllerObject;


        private void Awake() // Awake must be added to instantiate a Scene Controller as it extends monobehaviour it cannot be called outside its dependend object, i.e. a seperate gameobject for the scene controller MUST be in the scene at all times.
        {
            m_SceneControllerObject = GameObject.Find("SceneControllerObject"); // Used to call script attached to this in Start();
            //Debug.Log("woken");

        }
        private void Start()
        {
            m_SceneController = m_SceneControllerObject.GetComponent<SceneController>(); // This has to be called from a seperate object in the scene, this cannot be done in the same execution therfore we use Awake();!
            //Debug.Log("started");
        }


        private Quaternion m_CharacterTargetRot;
        private Quaternion m_CameraTargetRot;
        private bool m_cursorIsLocked = true;

        public void Init(Transform character, Transform camera)
        {
            Awake();
            Start();
            m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = camera.localRotation;
        }


        public void LookRotation(Transform character, Transform camera)
        {
            float yRot = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
            float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;

            m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
            m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

            if(clampVerticalRotation)
                m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

            if(smooth)
            {
                character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
                    smoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,
                    smoothTime * Time.deltaTime);
            }
            else
            {
                character.localRotation = m_CharacterTargetRot;
                camera.localRotation = m_CameraTargetRot;
            }

            UpdateCursorLock();
        }

        public void SetCursorLock(bool value)
        {
            lockCursor = value;
            if(!lockCursor)
            {//we force unlock the cursor if the user disable the cursor locking helper
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void UpdateCursorLock()
        {
            //if the user set "lockCursor" we check & properly lock the cursos
            if (lockCursor)
                InternalLockUpdate();
        }

        private void InternalLockUpdate()
        {

            if (Input.GetKeyUp(KeyCode.Escape) || /*Time.timeScale == 0.0f*/ m_SceneController.getPauseState() || Input.GetKeyDown(KeyCode.Pause)) //Time.timeScale == 0.0f || Input.GetKeyDown(KeyCode.Pause) arguments are added to provide a cursor when the game is paused.
            {
                m_cursorIsLocked = false;
            }
            else if(Input.GetMouseButtonUp(0) || /*Time.timeScale != 0.0f*/ !m_SceneController.getPauseState()) //In order to make the cursor instantly invisible again after the pause is lifted, we must add Time.timeScale != 0.0f here
            {
                m_cursorIsLocked = true;
            }

            if (m_cursorIsLocked || /*Time.timeScale != 0.0f*/ !m_SceneController.getPauseState() && m_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (!m_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

    }
}
