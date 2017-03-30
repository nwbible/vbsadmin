using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VBSAdmin.Helpers
{
    [HtmlTargetElement("input", Attributes = "asp-for", TagStructure = TagStructure.WithoutEndTag)]
    public class DefaultDateTimeTagHelper : TagHelper
    {
        private const string ValueAttribute = "value";
        private DateTime DefaultDateTime = DateTime.MinValue;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (output.Attributes[ValueAttribute] != null)
            {
                var value = output.Attributes[ValueAttribute].Value;
                if (value != null)
                {
                    DateTime dt;
                    if (DateTime.TryParse(value.ToString(), out dt)
                        && dt.Date == DefaultDateTime)
                    {
                        output.Attributes.SetAttribute(ValueAttribute, "");
                    }
                }
            }
        }
    }
}
