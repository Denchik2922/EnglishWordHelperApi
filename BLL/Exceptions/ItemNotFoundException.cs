using System;

namespace BLL.Exceptions
{
	class ItemNotFoundException : Exception
    {
        public ItemNotFoundException() : base() { }
        public ItemNotFoundException(string message) : base(message) { }
    }
}
