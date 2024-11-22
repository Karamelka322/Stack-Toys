using CodeBase.Logic.General.Unity.Toys;
using UnityEngine;

namespace CodeBase.Logic.Interfaces.Scenes.Company.Systems.Levels
{
    public interface ILevelBorderSystem
    {
        Vector3 BottomLeftPoint { get; }
        Vector3 BottomRightPoint { get; }
        Vector3 TopLeftPoint { get; }
        Vector3 TopRightPoint { get; }
        Vector3 Clamp(ToyMediator toyMediator, Vector3 position);
    }
}