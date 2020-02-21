using BFS_c_sharp.Model;
using System;
using System.Collections.Generic;

namespace BFS_c_sharp
{
    class Program
    {
        public static void Main(string[] args)
        {
            Searcher graphSearcher = new Searcher();
            List<UserNode> users = graphSearcher.Users;
            
            int searchDistance = 4;
            UserNode user1 = users[18];
            UserNode user2 = users[35];

            int distance = graphSearcher.CountMinimumDistance(user1, user2);

            Console.WriteLine($"The distance between {user1} and {user2} is {distance}");

            graphSearcher.ClearVisitedNodes(users);

            Console.WriteLine($"Friends of {user1} at given distance ({searchDistance}):\n");
            
            List<UserNode> friendsAtDistance = graphSearcher.ListFriendsAtDistance(searchDistance, user1);
            if (friendsAtDistance.Count != 0)
            {
                foreach (UserNode friend in friendsAtDistance)
                {
                    Console.WriteLine(friend);
                }
            }
            else
            {
                Console.WriteLine("No friend-of-friend at that distance.");
            }

            graphSearcher.ClearVisitedNodes(users);

            Console.WriteLine($"\nGet shortest path between {user1} and {user2}: ");
            
            List<UserNode> userPath = graphSearcher.GetShortestPath(user1, user2);
            
            if (userPath.Count != 0)
            {
                foreach (UserNode user in userPath)
                {
                    Console.WriteLine(user);
                }
            }
            else
            {
                Console.WriteLine("No friend path was found.");
            }
            Console.ReadKey();
        }

        
    }
}
