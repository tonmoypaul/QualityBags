using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QualityBags.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace QualityBags.ViewModels
{
    public class BagViewModel
    {

        public Bag Bag { get; set; }
        //[FileExtensions(Extensions = "jpg")]
        public IFormFile ImageFile { get; set; }
    }
}
