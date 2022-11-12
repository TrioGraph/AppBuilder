using <<namespace>>.Models.Domain;
using <<namespace>>.Models.DTO;

namespace <<namespace>>.Repositories
{
    public interface <<RepositoryInterfaceName>>Repository
    {
        Task<List<<<FunctionName>>DTO>> get<<FunctionName>>();
        
        Task<IEnumerable<object>> get<<FunctionName>>DDL();
		
        Task<<<FunctionName>>> get<<FunctionName>>ById(string id);

        Task<<<FunctionName>>DTO> add<<FunctionName>>(<<FunctionName>> appsPermissions);

        Task<IEnumerable<<<FunctionName>>>> delete<<FunctionName>>(string roles);

        Task<<<FunctionName>>DTO> update<<FunctionName>>(string Id, <<FunctionName>> role);
       
    }
}