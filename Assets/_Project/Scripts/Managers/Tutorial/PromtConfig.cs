using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PromtConfig 
{
    [Header("General Settings")]
    [SerializeField] private Image image;
    [SerializeField] private Button button;
    [SerializeField] private float lifeDuration;
    [SerializeField] private int priority;

    [Header("Text Pulse Setings")]
    [SerializeField] private List<TMP_Text> text;
    [SerializeField] private float scaleSize;
    [SerializeField] private float pulseDuration;

    public bool HasText => text != null;
    public bool HasButton => button != null;
    public int Priority => priority;
    

    public Image Image => image;
    public List<TMP_Text> Texts => text;
    public Button Button => button;
    public float LifeDuration => lifeDuration;
    public float ScaleSize => scaleSize;
    public float PulseDuration => pulseDuration;
}
