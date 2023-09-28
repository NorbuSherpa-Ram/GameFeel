using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public struct FrameInput
{
    public Vector2 Move;
    public bool Jump;
}

public class PlayerInput : MonoBehaviour
{
    public PlayerInputAction _playeInputAction;

    public FrameInput frameInput { get; private set; }

    private InputAction move;
    private InputAction jump;

    private void Awake()
    {
        _playeInputAction = new PlayerInputAction();
        move = _playeInputAction.Player.Move;
        jump = _playeInputAction.Player.Jump;
    }
    private void OnEnable()
    {
        _playeInputAction.Enable();
    }
    private void OnDisable()
    {
        _playeInputAction.Disable();
    }

    void Update()
    {
        frameInput = GatherInput();
    }

    private FrameInput GatherInput()
    {
        return new FrameInput
        {
            Move = move.ReadValue<Vector2>(),
            Jump = jump.WasPerformedThisFrame()
        };
    }
}
