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

            ClearVisitedNodes(users);

            Console.WriteLine("\nGet shortest path between two users: ");
            List<UserNode> userPath = GetShortestPath(user1, user2);
            
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

        public static int ListMinimumDistance(UserNode user, UserNode friend)
        {
            Console.WriteLine($"\nUser1: {user}\nUser2: {friend}");

            UserNode rootUser = user;
            rootUser.ParentNode = new UserNode("Test", "User") { Id = -1 };
            rootUser.IsVisited = true;

            rootUser = GetFriendOfSearchedFriend(rootUser, friend);
            friend.ParentNode = rootUser;
            int distance = GetDistance(1, friend, user);
            
            return distance;
        }

        public static UserNode GetFriendOfSearchedFriend(UserNode rootUser, UserNode friend)
        {
            Queue<UserNode> users = new Queue<UserNode>();

            while (!rootUser.Friends.Contains(friend))
            {
                foreach (UserNode user in rootUser.Friends)
                {
                    if (!user.IsVisited)
                    {
                        user.ParentNode = rootUser;
                        users.Enqueue(user);
                        user.IsVisited = true;
                    }
                }

                rootUser = users.Dequeue();
            }
            return rootUser;
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
    }
}
