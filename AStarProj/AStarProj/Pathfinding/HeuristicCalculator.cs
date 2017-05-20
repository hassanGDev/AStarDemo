using System;
using System.Collections.Generic;
using System.Text;

namespace AStarProj.Pathfinding
{
    class HeuristicCalculator
    {
        private HeuristicTypes _hueristicType;

        /// <summary>
        /// Default constructor needs a hueristic, as we don't want it to change half way through
        /// </summary>
        /// <param name="heuristicType"></param>
        public HeuristicCalculator(HeuristicTypes heuristicType)
        {
            this._hueristicType = heuristicType;
        }

        public float CalculateHeuristic(float x, float y, float targetX, float targetY)
        {
            switch(this._hueristicType)
            {
                case HeuristicTypes.Manhattan:
                    return CalculateManhattan(x, y, targetX, targetY);
                case HeuristicTypes.Pythagorean:
                    return CalculatePythagorean(x, y, targetX, targetY);
                case HeuristicTypes.InfatedPythagorean:
                    return CalculateInflatedPythagorean(x, y, targetX, targetY);
            }

            //none
            return 0.0f;
        }

        private float CalculateManhattan(float x, float y, float targetX, float targetY)
        {
            return Math.Abs(x - targetX) + Math.Abs(y - targetY);
        }

        /// <summary>
        /// This is ultra expensive as it has a _root function
        /// </summary>
        private float CalculatePythagorean(float x, float y, float targetX, float targetY)
        {
            return (float)Math.Sqrt(CalculateInflatedPythagorean(x, y, targetX, targetY));
        }

        /// <summary>
        /// Based on a cheaper but larger calculation, it could be poor when the distances are large and very similar
        /// </summary>
        private float CalculateInflatedPythagorean(float x, float y, float targetX, float targetY)
        {
            double xSq = Math.Pow(x - targetX, 2);
            double ySq = Math.Pow(y - targetX, 2);

            return (float)(xSq + ySq);
        }

    }
}
