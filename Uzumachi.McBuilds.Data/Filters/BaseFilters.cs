using Dapper;
using System.Collections.Generic;
using Uzumachi.McBuilds.Data.Helpers;

namespace Uzumachi.McBuilds.Data.Filters {

  public abstract class BaseFilters {

    public int Limit { get; set; }

    public int Offset { get; set; }

    public string Sorting { get; set; }


    protected int MaxLimit = 200;

    protected int DefaultLimit = 20;

    protected string DefaultSorting = "by-date";

    protected Dictionary<string, string> ValidSortings = new() {
      { "by-date", "base.create_date DESC" },
      { "by-date-asc", "base.create_date" }
    };


    public abstract string GetWhereSql(DynamicParameters parameters, bool needAND = false);

    public string GetOrderSql(bool needOrderBy = true) {
      string sorting = GetActiveSorting();

      string orderBy = ValidSortings.ContainsKey(sorting)
        ? ValidSortings[sorting]
        : ValidSortings[DefaultSorting];

      return (needOrderBy ? " ORDER BY " : " ") + orderBy;
    }

    public string GetLimitSql() {
      int limit = Limit < 1 ? DefaultLimit : (Limit > MaxLimit ? MaxLimit : Limit);

      return SqlHelpers.GetLimitOffsetExpression(limit, Offset);
    }


    protected string GetActiveSorting() {
      return string.IsNullOrWhiteSpace(Sorting)
        ? DefaultSorting
        : Sorting.Trim().ToLower().Replace("-desc", "");
    }
  }
}
