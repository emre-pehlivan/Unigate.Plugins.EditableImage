using BingolUniversity.Model.DataModel;
using Mcms.Sdk;
using Mcms.UI.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mcms.UI.Framework.Web.Mvc.Routing;
using Mcms.UI.Framework.PluginAttribute;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using MCMS.Common.Models.DbModels;
using Mcms.UI.Framework.Utilities;
using MCMS.Common.Models.ViewModels;
using BLL;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.Hosting;
using Unigate.Plugins.EditableImage;
using Unigate.Plugins.EditableImage.Auth;
using MCMS.Common.Result;
using Mcms.UI.Framework.PluginArea;


namespace Unigate.Plugins.EditableImage.Controllers
{
    [UIController(Name = "Editable Images")]
    public class EditableImageController : Controller
    {
        [UIAction(Name="Editable Images")]
        public ActionResult Index()
        {
            var prop = PluginContext.Current.GetProperty("ViewName");
            
            return View(prop);
        }

        [HttpPost]
        [ExecutableAction()]
        public ActionResult Save(EditableImage image)
        {
            if (!EditableImageAuthorization.Authorization())
            {
                throw new Exception("Bu işlemi yapmak için yetkiniz yok.");
            }

            EditableImage existsImage = UnigateObject.Query("EditableImage")
                .WhereEqualTo("Code", image.Code)
                .FirstOrDefault<EditableImage>();

            Guid imageGuid = Guid.Empty;

            if (existsImage != null)
            {
                var update = UnigateObject.Update("EditableImage")
                    .Column("Code", image.Code)
                    .Column("ImageFile", image.ImageFile)
                    .WhereEqualTo("ContentId", existsImage.ContentId)
                    .Execute();

                if (update.ResultCode == ResultCode.Successfull)
                {
                    imageGuid = existsImage.ContentId;
                }
            }
            else
            {
                var insert = UnigateObject.Insert("EditableImage", image).Execute();
                if (insert.ResultCode == ResultCode.Successfull)
                {
                    imageGuid = Guid.Parse(insert.OutParameters.Get("ContentId"));
                }
            }

            return new JsonResult()
            {
                Data = imageGuid,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }

}