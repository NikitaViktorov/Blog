using BLL.DTOs;
using BLL.Exceptions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private Guid UserId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        [HttpGet]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> GetArticles()
        {
            try
            {
                return Ok(await _articleService.GetAll());
            }
            catch (ArticleException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("GetArticle/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetArticle([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _articleService.Get(id));
            }
            catch (ArticleException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [Route("ArticlesByTag/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetArticlesByTag([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _articleService.GetArticlesByTag(id));
            }
            catch (ArticleException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetArticleByText")]
        [HttpGet]
        public async Task<IActionResult> GetArticlesByText([FromBody] string text)
        {
            try
            {
                return Ok(await _articleService.GetArticleByText(text));
            }
            catch (ArticleException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("CreateArticle")]
        [HttpPost]
        public async Task<IActionResult> CreateArticle([FromBody] ArticleDTO articleDTO)
        {
            try
            {
                articleDTO.UserId = UserId;
                await _articleService.Create(articleDTO);
                return Ok();
            }
            catch (ArticleException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle([FromRoute] Guid id, [FromBody] ArticleDTO articleDTO)
        {
            try
            {
                await _articleService.Update(id, articleDTO);
                return Ok();
            }
            catch (ArticleException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("DeleteArticle/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteArticle([FromRoute] Guid id)
        {
            try
            {
                await _articleService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
