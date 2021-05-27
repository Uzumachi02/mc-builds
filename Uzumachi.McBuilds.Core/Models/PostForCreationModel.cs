using System;
using System.ComponentModel.DataAnnotations;

namespace Uzumachi.McBuilds.Core.Models {

  /// <summary>
  /// Model for create post
  /// </summary>
  public class PostForCreationModel {

    /// <summary>
    /// user id
    /// </summary>
    public int UserId { get; set; }

    [MinLength(5)]
    public string Description { get; set; }

    public DateTime? PublishDate { get; set; }
  }
}
