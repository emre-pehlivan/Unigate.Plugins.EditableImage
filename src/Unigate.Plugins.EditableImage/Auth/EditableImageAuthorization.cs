﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mcms.UI.Framework.Repository;
using Mcms.UI.Framework.Utilities;
using MCMS.Common.Runtime;

namespace Unigate.Plugins.EditableImage.Auth
{
    public class EditableImageAuthorization
    {
        public static bool Authorization()
        {
            var roleRepository = DependencyResolver.Current.GetService<IRoleRepository>();
            var role = roleRepository.SelectRole("Admin");

            var currentUser = UserUtility.CurrentUser;
            if (role != null && currentUser != null)
            {
                if (currentUser.UserRoleSites.Any(x => x.RoleId == role.Id))
                {
                    return true;
                }
            }

            return false;
        }
    }
}