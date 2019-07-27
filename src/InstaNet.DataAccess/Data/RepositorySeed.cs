using InstaNet.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace InstaNet.DataAccess.Data
{
    public class RepositorySeed
    {
        private static Profile profile;
        private static Post post;

        public static async Task<Profile> CreateSeed(RepositoryContext repositoryContext)
        {
            await repositoryContext.Profiles.AddRangeAsync(GetSeedProfils());
            await repositoryContext.Posts.AddRangeAsync(GetSeedPosts());
            await repositoryContext.Pictures.AddRangeAsync(GetSeedPictures());
            await repositoryContext.Replays.AddRangeAsync(GetSeedReplays());

            return profile;
        }

        private static IList<Profile> GetSeedProfils()
        {
            profile = new Profile
            {
                Id = Guid.NewGuid(),
                UserName = "Moritz Müller",
                DisplayName = "Moritz Müller",
                Bio = "First bio on InstaNet",
                Image = File.ReadAllBytes(".//wwwroot//Images//SampleUser.PNG")
            };

            var seedList = new List<Profile>
            {
                profile
            };

            return seedList;
        }

        private static IList<Post> GetSeedPosts()
        {
            post = new Post
            {
                Profile = profile,
                Caption = "The First Post"
            };

            var seedList = new List<Post>
            {
                post
            };

            return seedList;
        }

        private static IList<Picture> GetSeedPictures()
        {
            var seedList = new List<Picture>
            { 
                new Picture
                {
                    File = File.ReadAllBytes(".//wwwroot//Images//Sample.jpg"),
                    Post = post
                }
            };
            return seedList;
        }
        private static IList<Replay> GetSeedReplays()
        {
            var seedList = new List<Replay>
            {
                new Replay
                {
                    Post = post,
                    Profile = profile,
                    Text = "The First Replay"
                }
            };
            return seedList;
        }
    }
}
