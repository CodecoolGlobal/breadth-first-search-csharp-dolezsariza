﻿using System.Collections.Generic;

namespace BFS_c_sharp.Model
{
    public class UserNode
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        private readonly HashSet<UserNode> _friends = new HashSet<UserNode>();
        public bool IsVisited { get; set; }
        public UserNode ParentNode { get; set; }
        public int DistanceFromAnotherUser { get; set; }

        public HashSet<UserNode> Friends
        {
            get { return _friends; }
        }


        public UserNode() 
        {
            IsVisited = false;
        }

        public UserNode(string firstName, string lastName)
        {
            IsVisited = false;
            FirstName = firstName;
            LastName = lastName;
        }

        public void AddFriend(UserNode friend)
        {
            Friends.Add(friend);
            friend.Friends.Add(this);
        }

        public override string ToString()
        {
            return FirstName + " " + LastName + "(" + Friends.Count + ")";
        }
    }
}
