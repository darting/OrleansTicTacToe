using System;

namespace GrainInterfaces.Exceptions
{
    [Serializable]
    public abstract class BaseException : Exception
    {
        public BaseException(int code)
        {
            Code = code;
        }

        public int Code { get; }
    }

    [Serializable]
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException() : base(401)
        {
        }
    }

    [Serializable]
    public class CannotJoinGameException : BaseException
    {
        public CannotJoinGameException() : base(460)
        {
        }
    }

    [Serializable]
    public class NotInAnyGameException : BaseException
    {
        public NotInAnyGameException() : base(461)
        {
        }
    }

    [Serializable]
    public class NotYourTurnException : BaseException
    {
        public NotYourTurnException() : base(462)
        {
        }
    }

    [Serializable]
    public class YouAreNotInTheGameException : BaseException
    {
        public YouAreNotInTheGameException() : base(463)
        {
        }
    }

    [Serializable]
    public class GameNotYetStartException : BaseException
    {
        public GameNotYetStartException() : base(464)
        {
        }
    }

    [Serializable]
    public class GameIsFinishedException : BaseException
    {
        public GameIsFinishedException() : base(465)
        {
        }
    }

    [Serializable]
    public class InvalidMoveException : BaseException
    {
        public InvalidMoveException() : base(466)
        {
        }
    }
}
