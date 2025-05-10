using System.Collections.Generic;
using UnityEngine;

public class UIScaling : MonoBehaviour, IUIAnimateable
{
    [SerializeField] private List<float> scales = new();
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private bool randomizedTarget;
    [SerializeField] private bool animateAlways;

    [SerializeField] private bool animating;
    [SerializeField] private int _targetIndex = 1;
    
    private bool _dying = false;

    void Start()
    {
    }

    void Update()
    {
        if (animating)
            UpdateAnimation();
    }

    public void StartAnimation()
    {
        animating = true;
    }

    public void StopAnimation()
    {
        if(_dying) Destroy(gameObject);
        
        animating = false;

        SwitchTargetIndex();

        if (animateAlways) StartAnimation();
    }

    public void UpdateAnimation()
    {
        transform.localScale = Vector3.Lerp(transform.localScale,
            new Vector3(scales[_targetIndex], scales[_targetIndex], scales[_targetIndex]), Time.deltaTime * speed);
        bool direction = transform.localScale.x > scales[_targetIndex];
        if ((direction && transform.localScale.x < scales[_targetIndex] + 0.01f) ||
            (!direction && transform.localScale.x > scales[_targetIndex] - 0.01f))
            StopAnimation();
    }

    private void SwitchTargetIndex()
    {
        if (!randomizedTarget) _targetIndex++;
        else _targetIndex = Random.Range(0, scales.Count).Equals(_targetIndex) ? _targetIndex : _targetIndex + 1;

        if (_targetIndex >= scales.Count) _targetIndex = 0; // Намеренно 
    }

    public void DIE()
    {
        _targetIndex = 0;
        animating = true;
        _dying = true;
    }
}