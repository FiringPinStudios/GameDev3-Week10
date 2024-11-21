using GameDevWithMarco.Managers;
using UnityEngine;
using GameDevWithMarco.VFX;

namespace GameDevWithMarco.Player
{
    public class Player_Collision : MonoBehaviour
    {
        //References
        private VfxManager vfx;
        private Ripple ripple;
        public UIManager ui;

        [SerializeField] GameEvent goodPackageCollected;
        [SerializeField] GameEvent badPackageCollected;
        [SerializeField] GameEvent lifePackageCollected;

        //Variables
        public bool greenCollected = false;

        void Start()
        {
            Initialisation();
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            ExecuteLogicBasedOnWhatWeHaveCollidedWith(collision);
            collision.gameObject.SetActive(false);
        }

        private void ExecuteLogicBasedOnWhatWeHaveCollidedWith(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case "GoodBox":
                    goodPackageCollected.Raise();
                    GameManager.Instance.GreenPackLogic();
                    vfx.GoodPickupParticles();
                    vfx.AddPointsPromptMethod();
                    ripple.RippleReaction();
                    AudioManager.Instance.GoodPickupSound();
                    greenCollected = true;
                    break;
                case "BadBox":
                    badPackageCollected.Raise();
                    GameManager.Instance.RedPackLogic();
                    vfx.CamShake();
                    vfx.BadPickupParticles();
                    vfx.SubtractPointsPromptMethod();
                    ui.MinusOneLifeFeedback();
                    AudioManager.Instance.BadPickupSound();
                    break;
                case "LifeBox":
                    lifePackageCollected.Raise();
                    GameManager.Instance.lives++;
                    ripple.RippleReaction();
                    ui.PlusOneLifeFeedback();
                    AudioManager.Instance.LifePickupSound();
                    break;
                default:
                    break;
            }
        }

        private void Initialisation()
        {
            vfx = FindObjectOfType<VfxManager>();
            ripple = FindObjectOfType<Ripple>();
            ui = FindObjectOfType<UIManager>();
        }
    }
}
