namespace SmartPOSX.Core.DTOs.Roles
{
    public class RoleMapperModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Permissions { get; set; } = string.Empty;
    }
}
