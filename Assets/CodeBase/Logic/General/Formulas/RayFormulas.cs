using CodeBase.Logic.Interfaces.General.Formulas;
using UnityEngine;

namespace CodeBase.Logic.General.Formulas
{
    public class RayFormulas : IRayFormulas
    {
        public bool IntersectsTriangle(Vector3 rayOrigin, Vector3 rayDirection, Vector3 v0, Vector3 v1, Vector3 v2)
        {
            Vector3 edge1 = v1 - v0;
            Vector3 edge2 = v2 - v0;

            Vector3 h = Vector3.Cross(rayDirection, edge2);
            float a = Vector3.Dot(edge1, h);

            if (a > -float.Epsilon && a < float.Epsilon)
                return false; // Луч параллелен плоскости

            float f = 1.0f / a;
            Vector3 s = rayOrigin - v0;
            float u = f * Vector3.Dot(s, h);

            if (u < 0.0f || u > 1.0f)
                return false;

            Vector3 q = Vector3.Cross(s, edge1);
            float v = f * Vector3.Dot(rayDirection, q);

            if (v < 0.0f || u + v > 1.0f)
                return false;

            // Пересечение произошло, рассчитываем точку пересечения
            float t = f * Vector3.Dot(edge2, q);

            if (t > float.Epsilon) // t > 0 означает, что пересечение произошло в пределах луча
            {
                return true;
            }

            return false;
        }
    }
}