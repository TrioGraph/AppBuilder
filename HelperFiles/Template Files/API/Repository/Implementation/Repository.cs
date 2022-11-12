using Microsoft.EntityFrameworkCore;
using <<namespace>>.Models.Data;
using <<namespace>>.Models.Domain;
using <<namespace>>.Models.DTO;

namespace <<namespace>>.Repositories
{
    public class <<RepositoryName>> : <<RepositoryInterfaceName>>
    {
        private readonly <<ProjectName>>DbContext <<ProjectInstanceName>>DbContext;

        public <<RepositoryName>>(<<ProjectName>>DbContext <<ProjectInstanceName>>DbContext)
        {
            this.<<ProjectInstanceName>>DbContext = <<ProjectInstanceName>>DbContext;
        }

        public async Task<List<<<FunctionName>>DTO>> get<<FunctionName>>()
        {
            return await <<ProjectInstanceName>>DbContext.<<FunctionName>>.Join(<<ProjectInstanceName>>DbContext.<<FunctionName>>, x => x.ApplicationId, y => y.Id, 
            (x, y) => new <<FunctionName>>DTO {
                // ApplicationId = x.ApplicationId,
                // ApplicationName = y.ApplicationName,
                // Guid = x.Guid,
                // Id = x.Id,
                // Name = x.Name,
                // Status = x.Status
            } ).OrderByDescending(d => d.Id).ToListAsync();
    
            // return await <<ProjectInstanceName>>DbContext.Auth_Roles.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> get<<FunctionName>>DDL()
        {
            return await <<ProjectInstanceName>>DbContext.<<FunctionName>>
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Name
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<<<FunctionName>>> get<<FunctionName>>ById(string id)
        {
            return await <<ProjectInstanceName>>DbContext.<<FunctionName>>.FirstOrDefaultAsync(x => x.Guid == id);
        }

        public async Task<<<FunctionName>>DTO> add<<FunctionName>>(<<FunctionName>> <<FunctionInstanceName>>)
        {
            await <<ProjectInstanceName>>DbContext.AddRangeAsync(<<FunctionInstanceName>>);
            await <<ProjectInstanceName>>DbContext.SaveChangesAsync();

            var <<FunctionInstanceName>>Details = this.<<ProjectInstanceName>>DbContext.<<FunctionName>>.First(a => a.Id == <<FunctionInstanceName>>.ApplicationId);
            return Map<<FunctionName>>ToDTO(<<FunctionInstanceName>>, <<FunctionInstanceName>>Details);
        }

        public async Task<IEnumerable<<<FunctionName>>>> delete<<FunctionName>>(string roles)
        {
            var list = await <<ProjectInstanceName>>DbContext.<<FunctionName>>.Where(s => roles == s.Guid).ToListAsync();
            <<ProjectInstanceName>>DbContext.<<FunctionName>>.RemoveRange(list);
            await <<ProjectInstanceName>>DbContext.SaveChangesAsync();
            return list;
        }

        public async Task<<<FunctionName>>DTO> update<<FunctionName>>(string Id, <<FunctionName>> <<FunctionInstanceName>>)
        {
            var <<FunctionInstanceName>>Details = await <<ProjectInstanceName>>DbContext.<<FunctionName>>.FirstOrDefaultAsync(x => x.Guid == Id);
            if (<<FunctionInstanceName>>Details == null)
            {
                return null;
            }
            // authRole.Name = role.Name;
            // authRole.ApplicationId = role.ApplicationId;
            // authRole.Status = role.Status;
            await <<ProjectInstanceName>>DbContext.SaveChangesAsync();

            var updated<<FunctionName>>Details = this.<<ProjectInstanceName>>DbContext.<<FunctionName>>.First(a => a.Id == <<FunctionInstanceName>>Details.ApplicationId);
            return MapRoleToDTO(<<FunctionInstanceName>>Details, updated<<FunctionName>>Details);
        }

        public <<FunctionName>>DTO Map<<FunctionName>>ToDTO(<<FunctionName>> authRole, AuthApps authApps)
        {
            return new <<FunctionName>>DTO() {
                // Guid = authRole.Guid,
                // Id = authRole.Id,
                // Name = authRole.Name,
                // ApplicationId = authRole.ApplicationId,
                // ApplicationName = authApps.ApplicationName,
                // Status = authRole.Status
                };
        }

    }
}