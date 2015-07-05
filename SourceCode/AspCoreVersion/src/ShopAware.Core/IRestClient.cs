using System.Threading.Tasks;

namespace ShopAware.Core
{
    public interface IRestClient
    {
        #region Public Methods

        Task<string> Execute(string url);

        #endregion
    }
}