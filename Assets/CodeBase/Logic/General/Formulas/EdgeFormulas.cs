using UnityEngine;

namespace CodeBase.Logic.Scenes.Company.Systems.Levels
{
    public class EdgeFormulas : IEdgeFormulas
    {
        public Vector3 GetClosestPoint(Vector3 point, Vector3 edgePointA, Vector3 edgePointB)
        {
            // Вектор от A до B
            Vector3 AB = edgePointB - edgePointA;

            // Вектор от A до P
            Vector3 AP = point - edgePointA;

            // Скалярное произведение
            float dotProduct = Vector3.Dot(AP, AB);
            float squaredLengthAB = AB.sqrMagnitude;

            // Проекция точки P на прямую AB
            float t = dotProduct / squaredLengthAB;

            // Если t лежит между 0 и 1, то проекция лежит внутри отрезка
            if (t < 0)
                return edgePointA; // Ближайшая точка - точка A
            else if (t > 1)
                return edgePointB; // Ближайшая точка - точка B
            else
                return edgePointA + t * AB; // Проекция на отрезок
        }
    }
}