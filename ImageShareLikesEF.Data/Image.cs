﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageShareLikesEF.Data
{
    public class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime UploadedDate { get; set; }
        public string Src { get; set; }
        public int Likes { get; set; }
    }
}
