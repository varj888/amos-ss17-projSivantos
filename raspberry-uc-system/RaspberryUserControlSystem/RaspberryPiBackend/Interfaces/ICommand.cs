using System;

namespace RaspberryPiBackend
{
    public interface ICommand
    {
        void execute(Object parameter);

        void undo();
    }
}
