using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageFX : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public Animator Animator;
    
    public void SetValue(float value)
    {
        Text.text = value.ToString("0.00");
        Animator.Play("DamageFXIn");
    }
}
