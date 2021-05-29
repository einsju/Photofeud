using Firebase;
using Photofeud.Abstractions;
using Photofeud.Authentication;
using System;

namespace Photofeud.Firebase
{
    public abstract class FirebaseError
    {
        public static AuthenticationResult AuthenticationError(AggregateException exception, ITranslator translator)
        {
            var firebaseException = exception.GetBaseException() as FirebaseException;
            return new AuthenticationResult { Code = AuthenticationResultCode.Error, ErrorMessage = translator.Translate($"{firebaseException.ErrorCode}") };
        }
    }
}
