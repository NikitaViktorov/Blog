using BLL.DTOs;
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
        public async Task<IActionResult> GetArticles()
        {
            try
            {
                return Ok(await _articleService.GetAll());
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
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
            catch (Exception ex)
            {
                return Problem(ex.Message);
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
            catch (Exception ex)
            {
                return Problem(ex.Message);
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
            catch (Exception ex)
            {
                return Problem(ex.Message);
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
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        //[Route("AddTag")]
        //[HttpPut("{title}")]
        //public async Task<IActionResult> AddTag([FromRoute]string title,[FromBody] TagDTO tagDTO)
        //{
        //    try
        //    {
        //        await _articleService.AddTag(title, tagDTO);
        //        return Ok();
        //    }
        //    catch(Exception ex)
        //    {
        //        return Problem(ex.Message);
        //    }
        //}
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
