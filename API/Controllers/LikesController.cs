using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LikesController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly ILikesRepository _likesRepository;
        public LikesController(IUserRepository userRepository, ILikesRepository likesRepository)
        {
            _likesRepository = likesRepository;
            _userRepository = userRepository;
            
        }

        [HttpPost("{username}")]
        public async Task<ActionResult> AddLike(string username)
        {
            var sourceUserId = User.GetUserId();
            var likedUser = await _userRepository.GetUserByUsernameAsync(username);
            var sourceUser = await _likesRepository.GetUserWithLikes(sourceUserId);

            if(likedUser == null) return NotFound();

            if(sourceUser.UserName == username) return BadRequest("You Cannot like yourself ");

            var userLike = await _likesRepository.GetUserLike(sourceUserId, likedUser.Id);

            if(userLike != null) return BadRequest("You already like this user");

            userLike =  new UserLike 
            {
                SourceUserId = sourceUserId,
                TargetUserId = likedUser.Id

            };

            sourceUser.LikeByUsers.Add(userLike);

            if( await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to like user");




        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LikeDto>>> GetUserLikes(string predicate)
        {
            var userId = User.GetUserId();
            var userLikes = await _likesRepository.GetUserLikes(predicate, userId);

            if(userLikes != null) return Ok(userLikes);

            return NotFound();


            return  Ok();
        }

       
    }
}