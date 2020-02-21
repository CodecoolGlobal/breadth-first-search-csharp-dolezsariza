using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFS_c_sharp.Model
{
    interface ISearcher
    {
        int CountMinimumDistance(UserNode user, UserNode friend);
        List<UserNode> ListFriendsAtDistance(int searchDistance, UserNode user);
        List<UserNode> GetShortestPath(UserNode user, UserNode friend);
    }
}
