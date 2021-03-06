using System;
using System.Collections.Generic;
using Aspian.Domain.AttachmentModel;
using Aspian.Domain.BaseModel;
using Aspian.Domain.CommentModel;
using Aspian.Domain.SiteModel;
using Aspian.Domain.TaxonomyModel;

namespace Aspian.Domain.PostModel
{
    public interface IPost : IEntitymeta
    {
        string Title { get; set; }
        string Subtitle { get; set; }
        string Excerpt { get; set; }
        string Content { get; set; }
        string Slug { get; set; }
        PostStatusEnum PostStatus { get; set; }
        bool CommentAllowed { get; set; }
        int Order { get; set; }
        int ViewCount { get; set; }
        PostTypeEnum Type { get; set; }
        bool IsPinned { get; set; }
        int PinOrder { get; set; }


        #region Navigation Properties
        ICollection<PostAttachment> PostAttachments { get; set; }
        Guid? ParentId { get; set; }
        Post Parent { get; set; }
        ICollection<Post> ChildPosts { get; set; }
        Guid SiteId { get; set; }
        Site Site { get; set; }
        ICollection<TaxonomyPost> TaxonomyPosts { get; set; }
        ICollection<Postmeta> Postmetas { get; set; }
        ICollection<PostHistory> PostHistories { get; set; }
        ICollection<Comment> Comments { get; set; }
        #endregion
    }

    public enum PostStatusEnum
    {
        Publish,
        Future,
        Draft,
        Pending,
        Private,
        Trash,
        AutoDraft,
        Inherit

    }

    public enum PostTypeEnum
    {
        Posts,
        Products,
        Pages
    }
}