using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using gov_moderator.Services;
using Microsoft.AspNetCore.Http;
using gov_moderator.Models;

namespace gov_moderator.Controllers
{
    [Route("api/images")]
    public class ImagesApiController : Controller
    {
        private ImageManager imageMgr;

        public ImagesApiController(ImageManager imageMgr)
        {
            this.imageMgr = imageMgr;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var imageList = await this.imageMgr.GetImages();
            return this.Ok(imageList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(string id)
        {
            var image = await this.imageMgr.GetImage(id);
            return this.Ok(image);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(ImageFileModel file, IFormFile uploadFile)
        {
            var imgFile = await this.imageMgr.UploadNewImageFile(file, uploadFile);
            return this.Ok(new { id = imgFile });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(string id)
        {
            await this.imageMgr.DeleteImage(id);
            return this.Ok();
        }

    }
}
