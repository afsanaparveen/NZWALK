using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.models.domains;
using NZWalks.API.models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        //as we uploading image
        //POST:/api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);
            if(ModelState.IsValid)
            {
                //dto to domain model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.File.FileName,
                    FileDescription = request.FileDescription,
                };
                //use repository to uplaod file
            }
            return BadRequest(ModelState);
            
        }
        //validating the uploaded file and then save it to database
        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            //checking for extension of file
            var allowExtensions = new string[] { ".jpeg", ".jpg", ".png" };
            if(!allowExtensions.Contains(Path.GetExtension(request.File.FileName))) 
            {
                ModelState.AddModelError("file", "UnsupportedFileExtension");
            }
            //size of the file in bytes 10 bytes
            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB.SO PLEASE UPLOAD A SMALLER SIZE file");

            }

        }
    }
}
