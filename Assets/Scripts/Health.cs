using UnityEngine;
using UnityEngine.UI;

namespace Zlodey
{
    public class Health : MonoBehaviour
    {
        public float Value;
        public Slider HealthSlider;

        private void Start()
        {
            Helper.InitSlider(HealthSlider, Value);
        }
    }
}