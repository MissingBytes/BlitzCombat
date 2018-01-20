using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;


namespace UnitySampleAssets._2D
{

    [RequireComponent(typeof(PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {

        public VirtualJoyStickL joyStickL;
        private PlatformerCharacter2D character;
        private bool jump;
         float TimeToJump = 0;
        [SerializeField] float JumpRate = 2f;
        private void Awake()
        {
            character = GetComponent<PlatformerCharacter2D>();
        }

        private void Update()
        {
             //jump = CrossPlatformInputManager.GetButtonDown("Jump");
            if (joyStickL.Vertical() > 0.6 && Time.time > TimeToJump)
            {
                jump = true;
                TimeToJump = Time.time + 1 / JumpRate;
                Debug.Log("Jumping");
            }
        }

        private void FixedUpdate()
        {
            // Read the inputs.

            bool crouch = false; //=Input.GetKey(KeyCode.LeftControl);
            if (joyStickL.Vertical() < -0.5)
            {
                crouch = true;
            }

            float h = joyStickL.Horizontal();
            //float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            character.Move(h, crouch, jump);
            jump = false;
            crouch = false;
        }

  
    }

}


