using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Mcms.Sdk;
using Unigate.Plugins.EditableImage.Auth;

namespace Unigate.Plugins.EditableImage
{
    public static class ImageExtensions
    {
        public static string EditableImage(this HtmlHelper helper, string fileCode, string cssclass = null)
        {
            var auth = EditableImageAuthorization.Authorization();

            EditableImage file = UnigateObject.Query("EditableImage")
                .WhereEqualTo("Code", fileCode).FirstOrDefault<EditableImage>();

            if (file == null)
            {
                file = new EditableImage();
                file.ImageFile = "/Modules/Plugins/Unigate.Plugins.EditableImage/Assets/Images/noimage.png";
                file.ContentId = new Guid();
            }

            var image = "<img id=\"" + file.ContentId + "\" class=\"" + cssclass + "\" src=\"" + file.ImageFile + "\" />";

            if (auth)
            {
                string pencil = "<img class=\"filebrowser pencil\" data-code=\"" + fileCode + "\" src=\"/Modules/Plugins/Unigate.Plugins.EditableImage/Assets/Images/pencil.png\">";
                return "<span class=\"editable-imageholder\">" + pencil + image + "</span>";
            }

            return image;
        }
    }
}
