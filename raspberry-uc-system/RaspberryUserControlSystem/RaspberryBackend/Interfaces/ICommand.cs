using System;

namespace RaspberryBackend
{
    public interface ICommand
    {
        void executeAsync(Object parameter);

    }
}
