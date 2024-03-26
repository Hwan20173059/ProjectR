using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using UnityEngine;
using AnimationState = Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts.AnimationState;

namespace Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts
{
    public class CharacterControls : MonoBehaviour
    {
        public Character AssetCharacter;
        public CharacterController Controller; // https://docs.unity3d.com/ScriptReference/CharacterController.html
        public float RunSpeed = 1f;
        public float JumpSpeed = 3f;
        public float CrawlSpeed = 0.25f;
        public float Gravity = -0.2f;
        public ParticleSystem MoveDust;
        public ParticleSystem JumpDust;

        private Vector3 _motion = Vector3.zero;
        private int _inputX, _inputY;
        private float _activityTime;
        
        /*
        public void Start()
        {
            AssetCharacter.SetState(AnimationState.Idle);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) AssetCharacter.Animator.SetTrigger("Attack");
            else if (Input.GetKeyDown(KeyCode.J)) AssetCharacter.Animator.SetTrigger("Jab");
            else if (Input.GetKeyDown(KeyCode.P)) AssetCharacter.Animator.SetTrigger("Push");
            else if (Input.GetKeyDown(KeyCode.H)) AssetCharacter.Animator.SetTrigger("Hit");
            else if (Input.GetKeyDown(KeyCode.I)) { AssetCharacter.SetState(AnimationState.Idle); _activityTime = 0; }
            else if (Input.GetKeyDown(KeyCode.R)) { AssetCharacter.SetState(AnimationState.Ready); _activityTime = Time.time; }
            else if (Input.GetKeyDown(KeyCode.B)) AssetCharacter.SetState(AnimationState.Blocking);
            else if (Input.GetKeyUp(KeyCode.B)) AssetCharacter.SetState(AnimationState.Ready);
            else if (Input.GetKeyDown(KeyCode.D)) AssetCharacter.SetState(AnimationState.Dead);

            // Builder characters only.
            else if (Input.GetKeyDown(KeyCode.S)) AssetCharacter.Animator.SetTrigger("Slash");
            else if (Input.GetKeyDown(KeyCode.O)) AssetCharacter.Animator.SetTrigger("Shot");
            else if (Input.GetKeyDown(KeyCode.F)) AssetCharacter.Animator.SetTrigger("Fire1H");
            else if (Input.GetKeyDown(KeyCode.E)) AssetCharacter.Animator.SetTrigger("Fire2H");
            else if (Input.GetKeyDown(KeyCode.C)) AssetCharacter.SetState(AnimationState.Climbing);
            else if (Input.GetKeyUp(KeyCode.C)) AssetCharacter.SetState(AnimationState.Ready);
            else if (Input.GetKeyUp(KeyCode.L)) AssetCharacter.Blink();

            if (Controller.isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    GetDown();
                }
                else if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    GetUp();
                }
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _inputX = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                _inputX = 1;
            }
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _inputY = 1;
                
                if (Controller.isGrounded)
                {
                    JumpDust.Play(true);
                }
            }
        }

        public void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (Time.frameCount <= 1)
            {
                Controller.Move(new Vector3(0, Gravity) * Time.fixedDeltaTime);
                return;
            }

            var state = AssetCharacter.GetState();

            if (state == AnimationState.Dead)
            {
                if (_inputX == 0) return;

                AssetCharacter.SetState(AnimationState.Running);
            }

            if (_inputX != 0)
            {
                Turn(_inputX);
            }

            if (Controller.isGrounded)
            {
                if (state == AnimationState.Jumping)
                {
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        GetDown();
                    }
                    else
                    {
                        AssetCharacter.Animator.SetTrigger("Landed");
                        AssetCharacter.SetState(AnimationState.Ready);
                        JumpDust.Play(true);
                    }
                }

                _motion = state == AnimationState.Crawling
                    ? new Vector3(CrawlSpeed * _inputX, 0)
                    : new Vector3(RunSpeed * _inputX, JumpSpeed * _inputY);

                if (_inputX != 0 || _inputY != 0)
                {
                    if (_inputY > 0)
                    {
                        AssetCharacter.SetState(AnimationState.Jumping);
                    }
                    else
                    {
                        switch (state)
                        {
                            case AnimationState.Idle:
                            case AnimationState.Ready:
                                AssetCharacter.SetState(AnimationState.Running);
                                break;
                        }
                    }
                }
                else
                {
                    switch (state)
                    {
                        case AnimationState.Crawling:
                        case AnimationState.Climbing:
                        case AnimationState.Blocking:
                            break;
                        default:
                            var targetState = Time.time - _activityTime > 5 ? AnimationState.Idle : AnimationState.Ready;

                            if (state != targetState)
                            {
                                AssetCharacter.SetState(targetState);
                            }

                            break;
                    }
                }
            }
            else
            {
                _motion = new Vector3(RunSpeed * _inputX, _motion.y);
                AssetCharacter.SetState(AnimationState.Jumping);
            }

            _motion.y += Gravity;

            Controller.Move(_motion * Time.fixedDeltaTime);

            AssetCharacter.Animator.SetBool("Grounded", Controller.isGrounded);
            AssetCharacter.Animator.SetBool("Moving", Controller.isGrounded && _inputX != 0);
            AssetCharacter.Animator.SetBool("Falling", !Controller.isGrounded && Controller.velocity.y < 0);

            if (_inputX != 0 && _inputY != 0 || AssetCharacter.Animator.GetBool("Action"))
            {
                _activityTime = Time.time;
            }

            _inputX = _inputY = 0;

            if (Controller.isGrounded && !Mathf.Approximately(Controller.velocity.x, 0))
            {
                var velocity = MoveDust.velocityOverLifetime;

                velocity.xMultiplier = 0.2f * -Mathf.Sign(Controller.velocity.x);

                if (!MoveDust.isPlaying)
                {
                    MoveDust.Play();
                }
            }
            else
            {
                MoveDust.Stop();
            }
        }

        private void Turn(int direction)
        {
            var scale = AssetCharacter.transform.localScale;

            scale.x = Mathf.Sign(direction) * Mathf.Abs(scale.x);

            AssetCharacter.transform.localScale = scale;
        }

        private void GetDown()
        {
            AssetCharacter.Animator.SetTrigger("GetDown");
        }

        private void GetUp()
        {
            AssetCharacter.Animator.SetTrigger("GetUp");
        }
        */
    }
        
}