using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PID.WebAPI.Models.Common
{
    public interface IEntity
    {
    }

    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; set; }
        public string Guid {get; set;}
        // DateTime CreatedDate { get; set; }
        // DateTime? UpdatedDate { get; set; }
        
        // int CreatedBy { get; set; }
        // int UpdatedBy { get; set; }
    }

    public abstract class BaseEntity<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
        public string Guid {get; set;}
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
    }

    public abstract class BaseEntity : BaseEntity<int>
    {
    }
}
