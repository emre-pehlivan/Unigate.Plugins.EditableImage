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
                file.ImageFile = "https://cdn.yemeksepeti.com/CategoryImages/TR_ISTANBUL/manti_evi_baglarbasi_big.gif";
                file.ContentId = new Guid();
            }

            var image = "<img id=\"" + file.ContentId + "\" class=\"" + cssclass + "\" src=\"" + file.ImageFile + "\" />";

            if (auth)
            {
                string pencil = "<img class=\"filebrowser pencil\" data-code=\"" + fileCode + "\" src=\"http://icons.iconarchive.com/icons/pixelmixer/basic/16/pencil-icon.png\">";
                return "<span class=\"editable-imageholder\">" + pencil + image + "</span>";
            }

            return image;
        }
    }
}
