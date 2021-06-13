using Dapper;
using System.Collections.Generic;
using Uzumachi.McBuilds.Data.Helpers;

namespace Uzumachi.McBuilds.Data.Filters {

  public class PostFilters : BaseFilters {

    public int UserId { get; set; }


    public override string GetWhereSql(DynamicParameters parameters, bool needAND = false) {
      var wheres = new List<string>();

      if( UserId > 0 ) {
        wheres.Add("base.user_id = @userId");
        parameters.Add("userId", UserId);
      }

      return SqlHelpers.WheresToSql(wheres, needAND);
    }
  }
}
