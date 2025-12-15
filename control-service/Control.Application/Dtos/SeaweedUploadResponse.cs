using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Application.Dtos
{
    public class SeaweedUploadResponse
    {
        public string fid { get; set; }
        public string url { get; set; }
        public string publicUrl { get; set; }
        public string name { get; set; }
        public long size { get; set; }
    }
}
