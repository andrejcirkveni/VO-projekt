using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "FighterInputConfig", menuName = "Scriptable Objects/FighterInputConfig")]
public class FighterInputConfig : ScriptableObject
{
    public InputAction move;     // -1 / +1
    public InputAction attack;
    public InputAction guard;
    public InputAction ability;
    public InputAction quickstep;
    public InputAction heavyModifier;
}