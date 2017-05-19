using System;

namespace RaspberryBackend
{
    public interface ICommand
    {
        void execute(Object parameter);

    }
}
