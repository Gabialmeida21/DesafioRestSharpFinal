using RestSharp;
using RestSharpNetCoreTemplate.Bases;
using RestSharpNetCoreTemplate.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RestSharpNetCoreTemplate.Steps
{
    public class ReadFileSteps : RequestBase
    {

        public static void DownloadPdfFile()
        {
            DownloadData(FileTypes.pdf);
        }
    }
}
