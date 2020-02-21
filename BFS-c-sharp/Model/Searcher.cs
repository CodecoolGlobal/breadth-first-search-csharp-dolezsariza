using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS_c_sharp.Model
{
    class Searcher : ISearcher
    {
        public List<UserNode> Users { get; }

        public Searcher (RandomDataGenerator generator)
        {
            Users = generator.Generate();
            SetIds();
        }
        public int CountMinimumDistance(UserNode user, UserNode friend)
        {
            UserNode rootUser = user;
            rootUser.ParentNode = new UserNode("Test", "User") { Id = -1 };
            rootUser.IsVisited = true;

            rootUser = GetFriendOfSearchedFriend(rootUser, friend);
            friend.ParentNode = rootUser;
            int distance = GetDistance(1, friend, user);

            return distance;
        }

        public List<UserNode> ListFriendsAtDistance(int searchDistance, UserNode user)
        {
            Queue<UserNode> friends = new Queue<UserNode>();
            List<UserNode> friendsAtDistance = new List<UserNode>();

            UserNode rootUser = user;
            rootUser.DistanceFromAnotherUser = 0;
            rootUser.ParentNode = new UserNode("Test", "User") { Id = -1 };
            rootUser.IsVisited = true;

            while (rootUser.DistanceFromAnotherUser <= searchDistance)
            {
                foreach (UserNode friend in rootUser.Friends)
                {
                    if (!friend.IsVisited)
                    {
                        friend.ParentNode = rootUser;
                        friend.DistanceFromAnotherUser = friend.ParentNode.DistanceFromAnotherUser + 1;
                        friends.Enqueue(friend);
                        if (friend.DistanceFromAnotherUser == searchDistance)
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
        public List<UserNode> GetShortestPath(UserNode user, UserNode friend)
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
        public UserNode GetFriendOfSearchedFriend(UserNode rootUser, UserNode friend)
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

        public int GetDistance(int distance, UserNode user, UserNode parent)
        {
            if (user.ParentNode != parent)
            {
                user = user.ParentNode;
                return GetDistance(++distance, user, parent);
            }
            return distance;
        }

        public void ClearVisitedNodes(List<UserNode> users)
        {
            foreach (UserNode user in users)
            {
                user.IsVisited = false;
            }
        }

        public void SetIds()
        {
            int counter = 0;
            foreach (UserNode user in Users)
            {
                user.Id = counter;
                counter++;
            }
        }
    }
}
