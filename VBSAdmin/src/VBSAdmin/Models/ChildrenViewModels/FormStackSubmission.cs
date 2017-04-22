using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VBSAdmin.Models.ChildrenViewModels
{
    public class FormStackSubmission
    {
        public string status { get; set; }

        public FormStackSubmissionResponse response { get; set; }

    }

    public class FormStackSubmissionResponse
    {
        public List<Submission> submissions { get; set; }
        public int pages { get; set; }

    }

    public class Submission
    {
        public int id { get; set; }
        public string timestamp { get; set; }

        public List<Item> data { get; set; }

    }

    public class Item
    {
        public string field { get; set; }
        public string value { get; set; }
    }
}
