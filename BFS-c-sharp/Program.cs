using BFS_c_sharp.Model;
using System;
using System.Collections.Generic;

namespace BFS_c_sharp
{
    class Program
    {
        public static void Main(string[] args)
        {
            RandomDataGenerator generator = new RandomDataGenerator();
            List<UserNode> users = generator.Generate();
            SetIds(users);
            
            int searchDistance = 3;
            UserNode user1 = users[40];
            UserNode user2 = users[7];
            List<UserNode> friendsAtDistance = ListFriendsAtDistance(searchDistance, user1);
            if (friendsAtDistance.Count != 0)
            {
                Console.WriteLine($"Friends of {user1} at given distance ({searchDistance}):\n");
                foreach (UserNode friend in friendsAtDistance)
                {
                    Console.WriteLine(friend);
                }
            }
            else
            {
                Console.WriteLine("No friend-of-friend at that distance.");
            }

            ClearVisitedNodes(users);
            int distance = ListMinimumDistance(user1, user2);

            Console.WriteLine("\nFound the friend! Distance is: " + distance);
            Console.ReadKey();
        }

        public static int ListMinimumDistance(UserNode user1, UserNode user2)
        {
            Console.WriteLine($"\nUser1: {user1}\nUser2: {user2}");

            UserNode rootUser = user1;
            rootUser.ParentNode = new UserNode("Test", "User") { Id = -1 };
            rootUser.IsVisited = true;
            Queue<UserNode> users = new Queue<UserNode>();

            while (!rootUser.Friends.Contains(user2))
            {
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


        public static List<UserNode> GetShortestPath(UserNode user, UserNode friend)
        {
            List<UserNode> userPath = new List<UserNode>();
            friend.ParentNode = GetFriendOfSearchedFriend(user, friend);
            UserNode tempUser = friend;
            while (tempUser != user)
            {
                userPath.Add(tempUser);
                tempUser = tempUser.ParentNode;
            }
            userPath.Add(tempUser);

            return userPath;
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
