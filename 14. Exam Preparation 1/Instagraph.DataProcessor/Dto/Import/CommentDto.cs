using System;
using System.Collections.Generic;
using System.Text;

namespace Instagraph.DataProcessor.Dto.Import
{
    public class CommentDto
    {
        public string Content { get; set; }

        public string User { get; set; }

        public int? PostId { get; set; }
    }
}
