using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Infrastructure.CQRS.Commands
{
    public class CommandResult
    {
        public bool IsSuccess { get; set; }

        public CommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public static CommandResult Success()
        {
            return new CommandResult(true);
        }

        public static CommandResult Error()
        {
            return new CommandResult(false);
        }
    }

    public class CommandResult<TResponse> : CommandResult
    {
        public string Message { get; set; }
        public TResponse Response { get; set; }

        public CommandResult(bool isSuccess, TResponse response) : base(isSuccess)
        {
            Response = response;
        }

        public CommandResult(bool isSuccess, string message) : base(isSuccess)
        {
            Message = message;
        }

        public static CommandResult<TResponse> Success(TResponse message)
        {
            return new CommandResult<TResponse>(true, message);
        }

        public static CommandResult<TResponse> Error(string message)
        {
            return new CommandResult<TResponse>(false, message);
        }
    }
}
