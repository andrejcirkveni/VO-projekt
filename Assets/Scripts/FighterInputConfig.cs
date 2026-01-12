using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "FighterInputConfig", menuName = "Scriptable Objects/FighterInputConfig")]
public class FighterInputConfig : ScriptableObject
{
    public InputActionAsset actions;
    public string move = "Move";
    public string attack = "Attack";
    public string guard= "Guard";
    public string ability = "Ability";
    public string quickstep = "Quickstep";
    public string heavyModifier = "Heavy";
}