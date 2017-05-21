using AStarProj.Pathfinding;
using AStarProj.World;
using System;

namespace AStarProj
{
    class Program
    {
        const bool ALLOW_DIAGONAL = true;

        static void Main(string[] args)
        {
            Map map;


            //build the map
            MapManager mapManager = new MapManager(10,5);
            map = mapManager.Create();
            Console.WriteLine(map.ToString());

            Console.ReadLine();

            //PathSearcher ps = new PathSearcher(EHeuristicTypes.InfatedPythagorean, ALLOW_DIAGONAL);
            PathSearcher psPythagDiag = new PathSearcher(EHeuristicTypes.Pythagorean, ALLOW_DIAGONAL);
            PathSearcher psManhattanDiag = new PathSearcher(EHeuristicTypes.Manhattan, ALLOW_DIAGONAL);
            PathSearcher psManhattanSQ = new PathSearcher(EHeuristicTypes.Manhattan, !ALLOW_DIAGONAL);

            Map pythagPathMap = psPythagDiag.SearchPath(map);
            Map manhattanDiagPathMap = psManhattanSQ.SearchPath(map);
            Map manhattanSQPathMap = psManhattanSQ.SearchPath(map);

            Console.WriteLine(pythagPathMap.ToString());
            Console.WriteLine(manhattanDiagPathMap.ToString());
            Console.WriteLine(manhattanSQPathMap.ToString());

            Console.ReadLine();
        }
    }
}