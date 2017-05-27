using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonFiles.TransferObjects
{

    class AsynchData
    {

        public CancellationTokenSource cancellationToken;

        public Object attribute;

        public Object parameter;



        public AsynchData(CancellationTokenSource cancellationToken, object parameter)
        {
            this.cancellationToken = cancellationToken;
            this.parameter = parameter;
        }

        public AsynchData(CancellationTokenSource cancellationToken, Object attribute, object parameter)
        {
            this.cancellationToken = cancellationToken;
            this.parameter = parameter;

        }
    }
}
