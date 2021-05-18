﻿using BLL.DTOs;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {

        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        [Route("GetTag/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetTag([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _tagService.Get(id));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            try
            {
                return Ok(await _tagService.GetAll());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] TagDTO tagDTO)
        {
            try
            {
                await _tagService.Create(tagDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag([FromRoute] Guid id, [FromBody] TagDTO tagDTO)
        {
            try
            {
                await _tagService.Update(id, tagDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [Route("DeleteArticle/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTag([FromRoute]Guid id)
        {
            try
            {
                await _tagService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}