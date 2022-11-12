namespace PID.WEBAPI.Models.DTO
{
    public class AuthRolesDTO
    {
        public string? Guid { get; set; }
       public int Id { get; set; }
        public string? Name { get; set; }
        public int? ApplicationId { get; set; }
        public string? ApplicationName { get; set; }
        public bool? Status { get; set; }
        
    }
}
