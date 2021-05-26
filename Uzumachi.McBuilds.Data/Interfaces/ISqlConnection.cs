using System;
using System.Data;

namespace Uzumachi.McBuilds.Data.Interfaces {

  public interface ISqlConnection : IDisposable {

    public IDbConnection DB { get; }

  }
}
