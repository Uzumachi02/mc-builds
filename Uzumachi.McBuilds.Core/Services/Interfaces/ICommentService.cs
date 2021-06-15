﻿using System.Threading;
using System.Threading.Tasks;
using Uzumachi.McBuilds.Core.Models;

namespace Uzumachi.McBuilds.Core.Services.Interfaces {

  public interface ICommentService {

    Task<int> CreateForPostAsync(CommentCreateModel comment, CancellationToken token);
  }
}
