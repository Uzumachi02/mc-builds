using System;

namespace Uzumachi.McBuilds.Core.Exceptions {

  public class NotFoundCoreException : Exception {

    public NotFoundCoreException() 
      : base() {
    }

    public NotFoundCoreException(string message)
      : base(message) {
    }

    public NotFoundCoreException(string message, Exception innerException)
      : base(message, innerException) {
    }

    public NotFoundCoreException(string name, object key)
      : base($"Entity '{name}' ({key}) was not found.") {
    }
  }
}
