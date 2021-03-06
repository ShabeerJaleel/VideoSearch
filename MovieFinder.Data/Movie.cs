//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MovieFinder.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Movie
    {
        public Movie()
        {
            this.MovieLinks = new HashSet<MovieLink>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public string UniqueID { get; set; }
        public string ImageUrl { get; set; }
        public System.DateTime ReleaseDate { get; set; }
        public string LanguageCode { get; set; }
        public string Description { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public int Version { get; set; }
        public int VersionChange { get; set; }
        public string ImageScrapperID { get; set; }
        public string ImageLocalUrl { get; set; }
        public int ViewCount { get; set; }
        public int LikeCount { get; set; }
    
        public virtual ICollection<MovieLink> MovieLinks { get; set; }
    }
}
