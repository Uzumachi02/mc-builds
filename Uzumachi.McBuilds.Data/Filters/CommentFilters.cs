using Dapper;
using System.Collections.Generic;
using Uzumachi.McBuilds.Data.Helpers;

namespace Uzumachi.McBuilds.Data.Filters {

  public class CommentFilters : BaseFilters {

    public int UserId { get; set; }

    public int ItemId { get; set; }

    public int ItemTypeId { get; set; }

    /// <summary>
    /// Идентификатор комментария, ветку которого нужно получить
    /// </summary>
    public int CommentId { get; set; }

    /// <summary>
    /// Идентификатор комментария, начиная с которого нужно вернуть список
    /// </summary>
    public int StartCommentId { get; set; }

    public CommentFilters() {
      DefaultSorting = "by-date-asc";
    }

    public override string GetWhereSql(DynamicParameters parameters, bool needAND = false) {
      var wheres = new List<string>();

      if( UserId > 0 ) {
        wheres.Add("base.user_id = @userId");
        parameters.Add("userId", UserId);
      }

      if( ItemId > 0 ) {
        wheres.Add("base.item_id = @itemId");
        parameters.Add("itemId", ItemId);
      }

      if( ItemTypeId > 0 ) {
        wheres.Add("base.item_type_id = @itemTypeId");
        parameters.Add("itemTypeId", ItemTypeId);
      }

      if( CommentId > 0 ) {
        wheres.Add("base.parent_id = @commentId");
        parameters.Add("commentId", CommentId);
      } else {
        wheres.Add("base.parent_id = 0");
      }

      if( StartCommentId > 0 ) {
        wheres.Add("base.id >= @startCommentId");
        parameters.Add("startCommentId", StartCommentId);
      }

      return SqlHelpers.WheresToSql(wheres, needAND);
    }
  }
}
