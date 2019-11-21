using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SimplePipeline
{
    public interface IServerAddressesFeature
    {
        ICollection<string> Addresses { get; }
    }

    public class ServerAddressesFeature : IServerAddressesFeature
    {
        public ICollection<string> Addresses { get; } = new Collection<string>();
    }
}
