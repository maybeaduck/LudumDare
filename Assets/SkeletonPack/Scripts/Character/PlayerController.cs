using System.Collections.Generic;
using UnityEngine;

namespace SkeletonEditor
{
    public enum AppearAnimationType
    {
        Appear1,
        Appear2
    }

    public class PlayerController : MonoBehaviour
    {
        public float mouseRotateSpeed = 0.3f;

        private Animator animator;
        private Quaternion initRotation;


        private int currentAnimation;
        private List<string> animations;
        private Dictionary<AppearAnimationType, string> appearAnimations;

        private bool startMouseRotate;
        private Vector3 prevMousePosition;

        public static PlayerController Instance { get; private set; }

        void Awake() {
            if (Instance != null) {
                Destroy(this.gameObject);
            }
            Instance = this;
        }

        void Start() {
            animator = GetComponent<Animator>();
            initRotation = transform.rotation;

            appearAnimations = new Dictionary<AppearAnimationType, string>()
            {
                {AppearAnimationType.Appear1, "Appear1" },
                {AppearAnimationType.Appear2, "Appear2" },
            };
            animations = new List<string>()
            {
                "Idle2",
                "Idle3",
                "Idle4",
                "Hit1",
                "Fall1",
                "Attack1h1",
                "Attack1h2",
                "Attack1h3",
                "Attack2h1",
                "Attack2h2",
                "Attack2h3",
                "AttackSpell1",
                "ArcFire1",
                "Laught1"
            };
        }

        void Update() {

            if (Input.GetMouseButtonDown(1)) {
                startMouseRotate = true;
                prevMousePosition = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(1)) {
                startMouseRotate = false;
            }
            if (Input.GetMouseButton(1)) {
                transform.Rotate(new Vector3(0, (Input.mousePosition.x - prevMousePosition.x) * mouseRotateSpeed, 0));
                prevMousePosition = Input.mousePosition;
            }

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (Mathf.Abs(h) > 0.001f)
                v = 0;


            if (!startMouseRotate) {
                if (h > 0.5f) {
                    transform.rotation = Quaternion.Euler(initRotation.eulerAngles + new Vector3(0, -90, 0));
                }
                if (h < -0.5f) {
                    transform.rotation = Quaternion.Euler(initRotation.eulerAngles + new Vector3(0, 90, 0));
                }
                if (v > 0.5f) {
                    transform.rotation = Quaternion.Euler(initRotation.eulerAngles + new Vector3(0, -180, 0));
                }
                if (v < -0.5f) {
                    transform.rotation = Quaternion.Euler(initRotation.eulerAngles);
                }
            }

            var speed = Mathf.Max(Mathf.Abs(h), Mathf.Abs(v));
            animator.SetFloat("speedv", speed);
        }

        public void OnNextAnimation()
        {
            currentAnimation++;
            if (currentAnimation == animations.Count) {
                currentAnimation = 0;
            }
            foreach (var animation in animations) {
                animator.ResetTrigger(animation);
            }
            animator.SetTrigger(animations[currentAnimation]);
        }

        public void OnPrevAnimation() {
            currentAnimation--;
            if (currentAnimation == -1) {
                currentAnimation = animations.Count - 1;
            }
            foreach (var animation in animations) {
                animator.ResetTrigger(animation);
            }
            animator.SetTrigger(animations[currentAnimation]);
        }

        public void PlayAnimation(AppearAnimationType anim)
        {
            animator.SetTrigger(appearAnimations[anim]);
        }


    }
}