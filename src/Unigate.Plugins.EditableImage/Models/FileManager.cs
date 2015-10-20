using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Unigate.Plugins.EditableImage
{
    public class FileManager
    {
        public string Root { get; private set; }

        public FileManager(string _root)
        {
            if (string.IsNullOrWhiteSpace(_root)) throw new Exception("Ana klasör adı belirtmeniz gerekir.");

            Root = _root;
            Root = HostingEnvironment.MapPath(_root);
        }

        public string[] Folders
        {
            get
            {
                return System.IO.Directory.GetDirectories(this.Root);
            }
        }
        public string[] Files
        {
            get
            {
                return System.IO.Directory.GetFiles(Root, "*.jpg|*.gif|*.jpeg|*.png|*.bmp");
            }
        }
    }
}