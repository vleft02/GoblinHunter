using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    private bool _doorOpen = false;
    private bool _interact = false;

    // ---------- //
    // Animations //
    // ---------- //

    private int CLOSED = Animator.StringToHash("Closed");
    private int CLOSE = Animator.StringToHash("Close");
    private int OPEN = Animator.StringToHash("Open");
    private int OPENED = Animator.StringToHash("Opened");
    
    private Animator _animator;

    private float _lockedStateTime;

    private int _currentState;

    // Start is called before the first frame update
    void Start()
    {
       // _promptMessage = "Press E to open the door";
        _animator = GetComponent<Animator>();

    }
    /*
    protected override void Interact()
    {
        _interact = true;
        Debug.Log("Interact with " + gameObject.name);
    }
    */
    private int LockState(int state, float lt)
    {
        this._lockedStateTime = Time.time + lt;
        return state;
    }

    private int GetState()
    {
        if (Time.time < _lockedStateTime) return _currentState;

        if (!_doorOpen)
        {
            if (_interact)
            {
                _interact = false;
                _doorOpen = !_doorOpen;

                return LockState(OPEN, 0.6f);

            }
            return CLOSED;
        }
        else
        {
            if (_interact)
            {
                _interact = false;
                _doorOpen = !_doorOpen;

                return LockState(CLOSE, 0.2f);

            }
            return OPENED;
        }
    }

    public void ChangeAnimationState(int state)
    {
        if (state == this._currentState) return;

        _animator.CrossFadeInFixedTime(state, 0, 0);
        this._currentState = state;
    }


    void Update()
    {
        var state = this.GetState();
        this.ChangeAnimationState(state);

    }

    void Interactable.Interact()
    {
        _interact = true;
        Debug.Log("Interact with " + gameObject.name);
    }

    public string GetPromptMessage()
    {
        return "Press E to open the door";
    }
}
