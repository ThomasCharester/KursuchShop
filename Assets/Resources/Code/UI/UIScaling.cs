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
        animating = false;
        
        SwitchTargetIndex();
        
        if(animateAlways) StartAnimation();
    }

    public void UpdateAnimation()
    {
        transform.localScale = Vector3.Lerp(transform.localScale,
            new Vector3(scales[_targetIndex], scales[_targetIndex], scales[_targetIndex]), Time.deltaTime * speed);
        
        if(Mathf.Abs(transform.localScale.x) - Mathf.Abs(scales[_targetIndex]) > 5f)
        {
            StopAnimation();
            Debug.Log("Banana");
        }
    }
    private void SwitchTargetIndex()
    {
        Debug.Log("Apple");
        if(!randomizedTarget) _targetIndex++;
        else _targetIndex = Random.Range(0, scales.Count).Equals(_targetIndex) ? _targetIndex : _targetIndex + 1;

        if(_targetIndex >= scales.Count) _targetIndex = 0; // Намеренно 
        Debug.Log(_targetIndex);
    }
}