using LiveMusicLovers.Web.UI.Core;
using LiveMusicLovers.Web.UI.Core.Dto;
using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace LiveMusicLovers.Web.UI.Controllers
{
    public class ProfileController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private string serverFolderPath;

        public ProfileController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            serverFolderPath = ConfigurationManager.AppSettings["UPLOAD_DIR"];
        }

        public ActionResult Profile()
        {
            var user = _unitOfWork.Users.GetArtistById(User.Identity.GetUserId());

            var userDto = new UserDto
            {
                Id = user.Id,
                image = user.image,
                Name = user.Name,
            };

            return View("Profile", userDto);
        }

        [HttpPost]
        public ActionResult UploadPicture(HttpPostedFileBase postedFile)
        {
            if (postedFile?.ContentLength <= 0)
                return View("GigForm", "Gig");

            var pictureName = Guid.NewGuid().ToString();
            pictureName += Path.GetExtension(postedFile.FileName);

            var route = Server.MapPath(serverFolderPath);

            route = route + @"\" + pictureName;

            postedFile.SaveAs(route);


            var imageUrl = $"{serverFolderPath}/{pictureName}";

            _unitOfWork.Users.UpdateUser(imageUrl, User.Identity.GetUserId());
            
            _unitOfWork.Complete();

            return RedirectToAction("Profile","Profile");
        }
    }
}