using BFS_c_sharp.Model;
using System;
using System.Collections.Generic;

namespace BFS_c_sharp
{
    class Program
    {
        public static void Main(string[] args)
        {
            int counter = 0;
            RandomDataGenerator generator = new RandomDataGenerator();
            List<UserNode> users = generator.Generate();

            foreach (var user in users)
            {
                user.Id = counter;
                counter++;
                //Console.WriteLine(user);
            }

            int distance = ListMinimumDistance(users[50], users[9]);
            Console.WriteLine("Found the friend! Distance is: " + distance);
            Console.ReadKey();
        }

        //Lists the minimum distance between two users returning an integer
        // (friends' distance should be 1 from each other).
        public static int ListMinimumDistance(UserNode user1, UserNode user2)
        {
            Console.WriteLine($"User1: {user1}");
            Console.WriteLine($"User2: {user2}");

            UserNode rootUser = user1;
            rootUser.ParentNode = new UserNode("Test", "User") { Id = -1 };
            Queue<UserNode> users = new Queue<UserNode>();

            while (!rootUser.Friends.Contains(user2))
            {
                rootUser.IsVisited = true;
                foreach (UserNode friend in rootUser.Friends)
                {
                    if (!friend.IsVisited)
                    {
                        friend.ParentNode = rootUser;
                        users.Enqueue(friend);
                        friend.IsVisited = true;
                    }
                }
                
                rootUser = users.Dequeue();
            }
            user2.ParentNode = rootUser;
            int distance = GetDistance(1, user2, user1);
            //Console.WriteLine("Found the friend! Distance is: " + distance + " the user that had the friend: " + rootUser);

            return distance;
        }
        public static int GetDistance(int distance, UserNode user, UserNode parent)
        {
            if (user.ParentNode != parent)
            {
                user = user.ParentNode;
                return GetDistance(++distance, user, parent);
            }
            return distance;
        }

        public static List<UserNode> ListFriendsAtDistance(int distance, UserNode user)
        {
            Queue<UserNode> friends = new Queue<UserNode>();
            List<UserNode> friendsAtDistance = new List<UserNode>();

            UserNode rootUser = user;
            rootUser.DistanceFromAnotherUser = 0;
            rootUser.ParentNode = new UserNode("Test", "User") { Id = -1 };
            rootUser.IsVisited = true;

            while (rootUser.DistanceFromAnotherUser <= distance)
            {
                foreach (UserNode friend in rootUser.Friends)
                {
                    if (!friend.IsVisited)
                    {
                        friend.ParentNode = rootUser;
                        friend.DistanceFromAnotherUser = friend.ParentNode.DistanceFromAnotherUser + 1;
                        friends.Enqueue(friend);
                        if (friend.DistanceFromAnotherUser == distance)
                        {
                            friendsAtDistance.Add(friend);
                        }
                        friend.IsVisited = true;
                    }
                }
                try
                {
                    rootUser = friends.Dequeue();
                } 
                catch (System.InvalidOperationException)
                {
                    break;
                }
            }
            return friendsAtDistance;
        }

        public static void ClearVisitedNodes(List<UserNode> users)
        {
            foreach (UserNode user in users)
            {
                user.IsVisited = false;
            }
        }

        public static void SetIds(List<UserNode> users)
        {
            int counter = 0;
            foreach (UserNode user in users)
            {
                user.Id = counter;
                counter++;
            }
        }

        public void GetShortestPath(UserNode user1, UserNode user2)
        {
            
        }
    }
}
