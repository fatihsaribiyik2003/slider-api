using MongoDB.Bson;
using SubuProtokol.Entities.Base;

namespace SubuProtokol.Entities.Mongo
{
    public class Category : EntityBase<ObjectId>
    {
        public string Name { get; set; }
        public int ProductCount { get; set; }
        public string Description { get; set; }
        //public string Description2 { get; set; }
    }
}
