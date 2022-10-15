using System.Threading.Tasks;
using WowStats.Common.Models.User;

namespace WowStats.Common.Services.Abstractions
{
    public interface IWarshipsUserService
    {
		/// <summary>
		/// Gets the user that corresponds to the supplied userId.
		/// </summary>
		/// <param name="userId">The id to match against the user</param>
		/// <returns>The matching user, otherwise null.</returns>
		Task<WarshipsUser?> GetWarshipsUserAsync(string userId);
    }
}
