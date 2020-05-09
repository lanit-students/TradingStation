using System;

namespace Kernel
{
    public static class ErrorMessageFormatter
    {
        public static (Guid?, Guid?, string) GetMessageData(string str)
        {
            var items = str.Split('_', 3);

            if (Guid.TryParse(items[0], out var errorId))
            {
                var id = errorId;

                if (items.Length > 1 && Guid.TryParse(items[1], out var errorParentId))
                {
                    return (id, errorParentId, items.Length == 2 ? null : items[2]);
                }

                var errorMessage = "";

                if (items.Length == 2)
                {
                    errorMessage = items[1];
                }

                if (items.Length == 3)
                {
                    errorMessage = items[1] + items[2];
                }

                return (id, null, errorMessage);
            }

            return (null, null, str);
        }
    }
}
