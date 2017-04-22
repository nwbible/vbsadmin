using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VBSAdmin.Models.ChildrenViewModels
{
    public class FormStackFormResponse
    {
        public Response response { get; set; }
        public string status { get; set; }
    }

    public class Response
    {
        public string name { get; set; }
        public string last_submission_id { get; set; }
        public string last_submission_time { get; set; }

        public List<Field> fields { get; set; }

    }

    public class Field
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
