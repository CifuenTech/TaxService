using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TaxService.Api.Models
{
    [Serializable]
    public class RemoteException : Exception
    {
        public RemoteException() : base() { }
        public RemoteException(string message) : base(message) { }
        public RemoteException(string message, Exception inner) : base(message, inner) { }

        protected RemoteException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
