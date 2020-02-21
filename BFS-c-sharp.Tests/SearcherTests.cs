using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFS_c_sharp.Model;
using NUnit.Framework;

namespace BFS_c_sharp.Tests
{
    [TestFixture]
    public class SearcherTests
    {
        private readonly Searcher _searcher = new Searcher();
        private List<UserNode> _users;

        [SetUp]
        public void SetUp()
        {
            _users = new List<UserNode>
            {
                new UserNode("Sanyi", "Sanyi"),
                new UserNode("Bela", "Bela"),
                new UserNode("Brandon", "Siska"),
                new UserNode("Sophie", "Varga"),
                new UserNode("Wallie", "Eva")
            };
            SeedData();
        }

        private void SeedData()
        {
            _users[0].AddFriend(_users[1]);
            _users[0].AddFriend(_users[3]);
            _users[1].AddFriend(_users[0]);
            _users[1].AddFriend(_users[4]);
            _users[2].AddFriend(_users[3]);
            _users[2].AddFriend(_users[4]);
            _users[3].AddFriend(_users[0]);
            _users[3].AddFriend(_users[2]);
            _users[3].AddFriend(_users[4]);
            _users[4].AddFriend(_users[1]);
            _users[4].AddFriend(_users[2]);
            _users[4].AddFriend(_users[3]);
        }

        [TearDown]
        public void TearDown()
        {
            _users = null;
        }

        [Test]
        public void DistanceGivenSanyiAndSophieReturn1()
        {
            var sanyi = _users[0];
            var sophie = _users[3];
            var expectedDistance = 1;
            Assert.AreEqual(expectedDistance, _searcher.CountMinimumDistance(sanyi, sophie));
        }

        [Test]
        public void DistanceGivenSanyiAndBrandonReturns2()
        {
            var sanyi = _users[0];
            var brandon = _users[2];
            int expectedDistance = 2;
            Assert.AreEqual(expectedDistance, _searcher.CountMinimumDistance(sanyi, brandon));
        }

        [Test]
        public void FriendsOfFriendsGivenSanyiAnd1ReturnBelaAndSophie()
        {
            var sanyi = _users[0];
            var bela = _users[1];
            var sophie = _users[3];
            List<UserNode> expected = new List<UserNode> { bela, sophie };
            Assert.AreEqual(expected, _searcher.ListFriendsAtDistance(1, sanyi));
        }

        [Test]
        public void FriendsOfFriendsGivenSanyiAnd2ReturnAnyone()
        {
            var sanyi = _users[0];
            List<UserNode> expected = new List<UserNode>
            {
                _users[4],
                _users[2]
            };
            Assert.AreEqual(expected, _searcher.ListFriendsAtDistance(2, sanyi));
        }

        [Test]
        public void ShortestPathGivenSanyiAndBrandonReturnSanyiSophieBrandon()
        {
            var sanyi = _users[0];
            var sophie = _users[3];
            var brandon = _users[2];
            var expected = new List<UserNode> { brandon, sophie, sanyi };
            Assert.AreEqual(expected, _searcher.GetShortestPath(sanyi, brandon));
        }
    }
}
