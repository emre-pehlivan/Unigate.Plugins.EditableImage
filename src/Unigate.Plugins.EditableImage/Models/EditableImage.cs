using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mcms.Sdk;

namespace Unigate.Plugins.EditableImage
{
    public class EditableImage : UnigateBaseList
    {
        public string Code { get; set; }
        public string ImageFile { get; set; }
    }
}
