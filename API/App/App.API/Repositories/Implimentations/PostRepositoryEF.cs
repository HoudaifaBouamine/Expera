﻿using App.API.Data;
using App.API.Entities;
using App.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.API.Repositories.Implimentations
{
    public class PostRepositoryEF : IPostRepository
    {
        private readonly AppDbContext _AppDbContext;
        public PostRepositoryEF(AppDbContext appDbContext)
        {
        
            this._AppDbContext = appDbContext;
        
        }

        public async Task<Post?> Create(Post post,IEnumerable<Tag> tags)
        {
            var e = await _AppDbContext.Posts.AddAsync(post);

            Post? thePost = e.Entity;

            await _AppDbContext.SaveChangesAsync();

            foreach(Tag tag in tags)
            {
                PostHaveTag h = new PostHaveTag();
                h.Tag_Id = tag.Tag_Id;
                h.Post_Id = thePost.Post_Id;

                await _AppDbContext.PostsHaveTags.AddAsync(h);
            }


            await _AppDbContext.SaveChangesAsync();

            return thePost;
        }

        public async Task<List<Tag>> GetTagsByIds(IEnumerable<int> tags_ids)
        {
            return await _AppDbContext.Tags.Where(t => tags_ids.Contains(t.Tag_Id)).ToListAsync();
        }

        public async Task<Post?> Read(int id)
        {
            return await _AppDbContext.Posts.Where(p => p.Post_Id == id).FirstOrDefaultAsync();
        }
        public async Task<List<Tag>> ReadPostTags(int id)
        {
            return await _AppDbContext.Tags.FromSql($"SELECT T.* FROM   Posts p  JOIN PostsHaveTags pht  ON p.Post_Id = PHT.Post_Id  JOIN Tags t  ON pht.Tag_Id = t.Tag_Id  where P.Post_Id = {id};").ToListAsync(); 
        }

        public async Task<List<Post>> ReadUserPostsAsync(int user_Id)
        {
            return await _AppDbContext.Posts.Where(p=>p.User_Id == user_Id).ToListAsync();
        }


    }
}
