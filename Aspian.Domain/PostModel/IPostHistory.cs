using System;
using Aspian.Domain.BaseModel;

namespace Aspian.Domain.PostModel
{
    public interface IPostHistory : IEntityBase, IEntityCreate, IEntityInfo
    {
        string Title { get; set; }
        string  Subtitle { get; set; }
        string Excerpt { get; set; }
        string Content { get; set; }
        string Slug { get; set; }
        Guid? ParentId { get; set; }
        PostStatusEnum PostStatus { get; set; }
        bool CommentAllowed { get; set; }
        int Order { get; set; }

        #region Navigaiton Properties
            Guid PostId { get; set; }
            Post Post { get; set; }
        #endregion
    }
}