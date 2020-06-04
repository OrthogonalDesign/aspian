using System;
using System.Collections.Generic;
using Aspian.Domain.BaseModel;
using Aspian.Domain.SiteModel;

namespace Aspian.Domain.TaxonomyModel
{
    public interface ITermTaxonomy : IEntitymeta
    {
        TaxonomyEnum Taxonomy { get; set; }
        string Description { get; set; }


        #region Navigaiton Properties
            Guid? ParentId { get; set; }
            TermTaxonomy Parent { get; set; }
            ICollection<TermTaxonomy> ChildTaxonomies { get; set; }
            ICollection<TermPost> TermPosts { get; set; }
            Term Term { get; set; }
            Guid SiteId { get; set; }
            Site Site { get; set; }
        #endregion
    }

    public enum TaxonomyEnum {
        nav_menu,
        category,
        tag
    }
}