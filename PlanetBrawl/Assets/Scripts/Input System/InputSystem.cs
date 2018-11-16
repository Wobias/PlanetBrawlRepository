using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; // Required in C#

#region InputEnums

public enum Button
{
    A,
    B,
    X,
    Y,
    Start,
    Back,
    LeftShoulder,
    RightShoulder
};

public enum Trigger
{
    Left,
    Right
};

public enum ThumbStick
{
    LeftX,
    LeftY,
    RightX,
    RightY
};

public enum DPad
{
    Up,
    Down,
    Left,
    Right
};

#endregion

public class InputSystem : MonoBehaviour
{
    public static InputSystem instance;

    static GamePadState[] prevStates = new GamePadState[4];
    static GamePadState[] currentStates = new GamePadState[4];


    void Awake()
    {
        if (instance == null)
            instance = this;

        for (int i = 0; i < currentStates.Length; i++)
        {
            currentStates[i] = GamePad.GetState((PlayerIndex)i);
            prevStates[i] = currentStates[i];
        }
    }

    void Update()
    {
        UpdateStates();
    }

    void UpdateStates()
    {
        for (int i = 0; i < currentStates.Length; i++)
        {
            prevStates[i] = currentStates[i];
            currentStates[i] = GamePad.GetState((PlayerIndex)i);
        }
    }

    public static bool PadConnected(int playerNr)
    {
        return currentStates[playerNr].IsConnected;
    }

    public static void Rumble(Vector2 strength, float time, int playerNr)
    {
        instance.StartCoroutine(instance.RumbleForTime(strength, time, (PlayerIndex)playerNr));
    }

    #region ButtonChecks

    public static bool ButtonDown(Button button, int playerNr)
    {
        switch (button)
        {
            case Button.A:
                if (currentStates[playerNr].Buttons.A == ButtonState.Pressed &&
                    prevStates[playerNr].Buttons.A == ButtonState.Released)
                {
                    return true;
                }
                break;
            case Button.B:
                if (currentStates[playerNr].Buttons.B == ButtonState.Pressed &&
                    prevStates[playerNr].Buttons.B == ButtonState.Released)
                {
                    return true;
                }
                break;
            case Button.X:
                if (currentStates[playerNr].Buttons.X == ButtonState.Pressed &&
                    prevStates[playerNr].Buttons.X == ButtonState.Released)
                {
                    return true;
                }
                break;
            case Button.Y:
                if (currentStates[playerNr].Buttons.Y == ButtonState.Pressed &&
                    prevStates[playerNr].Buttons.Y == ButtonState.Released)
                {
                    return true;
                }
                break;
            case Button.Start:
                if (currentStates[playerNr].Buttons.Start == ButtonState.Pressed &&
                    prevStates[playerNr].Buttons.Start == ButtonState.Released)
                {
                    return true;
                }
                break;
            case Button.Back:
                if (currentStates[playerNr].Buttons.Back == ButtonState.Pressed &&
                    prevStates[playerNr].Buttons.Back == ButtonState.Released)
                {
                    return true;
                }
                break;
            case Button.LeftShoulder:
                if (currentStates[playerNr].Buttons.LeftShoulder == ButtonState.Pressed &&
                    prevStates[playerNr].Buttons.LeftShoulder == ButtonState.Released)
                {
                    return true;
                }
                break;
            case Button.RightShoulder:
                if (currentStates[playerNr].Buttons.RightShoulder == ButtonState.Pressed &&
                    prevStates[playerNr].Buttons.RightShoulder == ButtonState.Released)
                {
                    return true;
                }
                break;
            default:
                break;
        }

        return false;
    }

    public static bool ButtonPressed(Button button, int playerNr)
    {
        switch (button)
        {
            case Button.A:
                if (currentStates[playerNr].Buttons.A == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case Button.B:
                if (currentStates[playerNr].Buttons.B == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case Button.X:
                if (currentStates[playerNr].Buttons.X == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case Button.Y:
                if (currentStates[playerNr].Buttons.Y == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case Button.Start:
                if (currentStates[playerNr].Buttons.Start == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case Button.Back:
                if (currentStates[playerNr].Buttons.Back == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case Button.LeftShoulder:
                if (currentStates[playerNr].Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case Button.RightShoulder:
                if (currentStates[playerNr].Buttons.RightShoulder == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            default:
                break;
        }

        return false;
    }

    public static bool ButtonUp(Button button, int playerNr)
    {
        switch (button)
        {
            case Button.A:
                if (currentStates[playerNr].Buttons.A == ButtonState.Released &&
                    prevStates[playerNr].Buttons.A == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case Button.B:
                if (currentStates[playerNr].Buttons.B == ButtonState.Released &&
                    prevStates[playerNr].Buttons.B == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case Button.X:
                if (currentStates[playerNr].Buttons.X == ButtonState.Released &&
                    prevStates[playerNr].Buttons.X == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case Button.Y:
                if (currentStates[playerNr].Buttons.Y == ButtonState.Released &&
                    prevStates[playerNr].Buttons.Y == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case Button.Start:
                if (currentStates[playerNr].Buttons.Start == ButtonState.Released &&
                    prevStates[playerNr].Buttons.Start == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case Button.Back:
                if (currentStates[playerNr].Buttons.Back == ButtonState.Released &&
                    prevStates[playerNr].Buttons.Back == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case Button.LeftShoulder:
                if (currentStates[playerNr].Buttons.LeftShoulder == ButtonState.Released &&
                    prevStates[playerNr].Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case Button.RightShoulder:
                if (currentStates[playerNr].Buttons.RightShoulder == ButtonState.Released &&
                    prevStates[playerNr].Buttons.RightShoulder == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            default:
                break;
        }

        return false;
    }

    #endregion

    #region DPadChecks

    public static bool DPadDown(DPad dPad, int playerNr)
    {
        switch (dPad)
        {
            case DPad.Up:
                if (currentStates[playerNr].DPad.Up == ButtonState.Pressed &&
                    prevStates[playerNr].DPad.Up == ButtonState.Released)
                {
                    return true;
                }
                break;
            case DPad.Down:
                if (currentStates[playerNr].DPad.Down == ButtonState.Pressed &&
                    prevStates[playerNr].DPad.Down == ButtonState.Released)
                {
                    return true;
                }
                break;
            case DPad.Left:
                if (currentStates[playerNr].DPad.Left == ButtonState.Pressed &&
                    prevStates[playerNr].DPad.Left == ButtonState.Released)
                {
                    return true;
                }
                break;
            case DPad.Right:
                if (currentStates[playerNr].DPad.Right == ButtonState.Pressed &&
                    prevStates[playerNr].DPad.Right == ButtonState.Released)
                {
                    return true;
                }
                break;
            default:
                break;
        }

        return false;
    }

    public static bool DPadPressed(DPad dPad, int playerNr)
    {
        switch (dPad)
        {
            case DPad.Up:
                if (currentStates[playerNr].DPad.Up == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case DPad.Down:
                if (currentStates[playerNr].DPad.Down == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case DPad.Left:
                if (currentStates[playerNr].DPad.Left == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case DPad.Right:
                if (currentStates[playerNr].DPad.Right == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            default:
                break;
        }

        return false;
    }

    public static bool DPadUp(DPad dPad, int playerNr)
    {
        switch (dPad)
        {
            case DPad.Up:
                if (currentStates[playerNr].DPad.Up == ButtonState.Released &&
                    prevStates[playerNr].DPad.Up == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case DPad.Down:
                if (currentStates[playerNr].DPad.Down == ButtonState.Released &&
                    prevStates[playerNr].DPad.Down == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case DPad.Left:
                if (currentStates[playerNr].DPad.Left == ButtonState.Released &&
                    prevStates[playerNr].DPad.Left == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            case DPad.Right:
                if (currentStates[playerNr].DPad.Right == ButtonState.Released &&
                    prevStates[playerNr].DPad.Right == ButtonState.Pressed)
                {
                    return true;
                }
                break;
            default:
                break;
        }

        return false;
    }

    #endregion

    #region TriggerChecks

    public static bool TriggerDown(Trigger trigger, int playerNr)
    {
        switch (trigger)
        {
            case Trigger.Left:
                if (currentStates[playerNr].Triggers.Left > 0 &&
                    prevStates[playerNr].Triggers.Left == 0)
                {
                    return true;
                }
                break;
            case Trigger.Right:
                if (currentStates[playerNr].Triggers.Right > 0 &&
                    prevStates[playerNr].Triggers.Right == 0)
                {
                    return true;
                }
                break;
            default:
                break;
        }

        return false;
    }

    public static bool TriggerPressed(Trigger trigger, int playerNr)
    {
        switch (trigger)
        {
            case Trigger.Left:
                if (currentStates[playerNr].Triggers.Left > 0)
                {
                    return true;
                }
                break;
            case Trigger.Right:
                if (currentStates[playerNr].Triggers.Right > 0)
                {
                    return true;
                }
                break;
            default:
                break;
        }

        return false;
    }

    public static bool TriggerUp(Trigger trigger, int playerNr)
    {
        switch (trigger)
        {
            case Trigger.Left:
                if (currentStates[playerNr].Triggers.Left == 0 &&
                    prevStates[playerNr].Triggers.Left > 0)
                {
                    return true;
                }
                break;
            case Trigger.Right:
                if (currentStates[playerNr].Triggers.Right == 0 &&
                    prevStates[playerNr].Triggers.Right > 0)
                {
                    return true;
                }
                break;
            default:
                break;
        }

        return false;
    }

    #endregion

    #region ThumbStickChecks

    public static float ThumbstickInput(ThumbStick thumbStick, int playerNr)
    {
        switch (thumbStick)
        {
            case ThumbStick.LeftX:
                return currentStates[playerNr].ThumbSticks.Left.X;
            case ThumbStick.LeftY:
                return currentStates[playerNr].ThumbSticks.Left.Y;
            case ThumbStick.RightX:
                return currentStates[playerNr].ThumbSticks.Right.X;
            case ThumbStick.RightY:
                return currentStates[playerNr].ThumbSticks.Right.Y;
            default:
                return 0;
        }
    }

    #endregion

    IEnumerator RumbleForTime(Vector2 strength, float time, PlayerIndex playerNr)
    {
        GamePad.SetVibration(playerNr, strength.x, strength.y);
        yield return new WaitForSeconds(time);
        GamePad.SetVibration(playerNr, 0, 0);
    }
}
