
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADB.SA.Reports.Entities.DTO;

namespace ADB.SA.Reports.View
{
    public interface ICommentsView
    {
        void RenderComments(string comments);
        void RenderComments(CommentResponseDTO response);
    }
}
