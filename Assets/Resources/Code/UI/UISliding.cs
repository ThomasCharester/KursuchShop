using System.Collections.Generic;
using UnityEngine;

public class UISliding : MonoBehaviour, IUIAnimateable
{
    [SerializeField] private List<RectTransform> positions;

    [SerializeField] private float speed = 1.0f;
    [SerializeField] private bool randomizedTarget;
    [SerializeField] private bool canBeRetargeted;
    
    [SerializeField] private bool animateAlways;
    [SerializeField] private bool animating;
    private int _targetIndex = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (animating) UpdateAnimation();
    }

    public void UpdateAnimation()
    {
        transform.position = Vector3.Lerp(transform.position, positions[_targetIndex].position, Time.deltaTime * speed);
        
        if(Vector3.Distance(transform.position, positions[_targetIndex].position) < 5f) StopAnimation();
    }

    public void StartAnimation()
    {
        animating = true;
        if(canBeRetargeted) SwitchTargetIndex();
    }

    public void StopAnimation()
    {
        animating = false;
        
        if(!canBeRetargeted) SwitchTargetIndex();
        
        if(animateAlways) StartAnimation();
    }

    private void SwitchTargetIndex()
    {
        if(!randomizedTarget) _targetIndex++;
        else _targetIndex = Random.Range(0, positions.Count).Equals(_targetIndex) ? _targetIndex : _targetIndex + 1;

        if(_targetIndex >= positions.Count) _targetIndex = 0; // Намеренно 
    }
}