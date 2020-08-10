﻿using System.Collections.Generic;
using Drexel.VidUp.Business;

namespace Drexel.VidUp.JSON
{
    public class DeserializationRepository
    {
        public static UploadList UploadList { get; set; }

        public static TemplateList TemplateList { get; set; }

        public static PlaylistList PlaylistList { get; set; }

        public static void ClearRepositories()
        {
            UploadList = null;
            TemplateList = null;
            PlaylistList = null;
        }
    }
}
