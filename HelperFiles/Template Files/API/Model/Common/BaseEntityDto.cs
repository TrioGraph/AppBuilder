namespace PID.WEBAPI.Models.Common
{
   
        public interface IEntity
        {
        }

        public interface IEntity<TKey> : IEntity
        {
        public Guid Guid { get; set; }
        public TKey Id { get; set; }
        public DateTime CreatedDate { get; set; }
            public DateTime? UpdatedDate { get; set; }
            public int CreatedBy { get; set; }
            public int UpdatedBy { get; set; }
        }


        public abstract class BaseEntityDto<TKey> : IEntity<TKey>
        {
        public Guid Guid { get; set; }
        public TKey Id { get; set; }
        public DateTime CreatedDate { get; set; }
            public DateTime? UpdatedDate { get; set; }
            public int CreatedBy { get; set; }
            public int UpdatedBy { get; set; }
        }

        public abstract class BaseEntityDto : BaseEntityDto<int>
        {
        }

    
}

