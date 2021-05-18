﻿using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            try
            {
                return Ok(await _commentService.GetAll());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _commentService.Get(id));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Route("CreateComment/{title}")]
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromRoute]string title, [FromBody] CommentDTO commentDTO)
        {
            try
            {
                await _commentService.Create(title, commentDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment([FromRoute] Guid id, [FromBody] CommentDTO commentDTO)
        {
            try
            {
                await _commentService.Update(id, commentDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [Route("DeleteComment/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id)
        {
            try
            {
                await _commentService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}