using PID.WebAPI.Models.Common;

namespace PID.WebAPI.Models.Domain
{
    public class AuthRoles : BaseEntity<int>
    {
        public string? Name { get; set; }
        public int? ApplicationId { get; set; }
        public bool? Status { get; set; }
    }
}
