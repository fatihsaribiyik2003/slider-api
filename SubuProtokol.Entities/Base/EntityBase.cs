namespace SubuProtokol.Entities.Base
{
    public abstract class EntityBase<T> : IEntity
    {
        public T Id { get; set; }

        //public DateTime CreatedAt { get; set; } = DateTime.Now;
        //public DateTime? ModifiedAt { get; set; }
        //public bool IsDeleted { get; set; }

        //public EntityBase()
        //{
        //    if (typeof(T) == typeof(Guid))
        //    {
        //        this.GetType().GetProperty(nameof(Id)).SetValue(this, Guid.NewGuid());
        //    }
        //}
    }
}
