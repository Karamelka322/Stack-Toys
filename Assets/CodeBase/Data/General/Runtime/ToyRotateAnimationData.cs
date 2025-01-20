using UniRx;
using UnityEngine;

namespace CodeBase.Data.General.Runtime
{
    public class ToyRotateAnimationData
    {
        public CompositeDisposable CompositeDisposable;
        public Vector3 StartScale;

        public Vector3 Scale;
        public Vector3 RotationAxis;
    }
}