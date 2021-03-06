using System;

namespace Uzumachi.McBuilds.Domain.Entities {

  public class UserEntity {

    public const string TABLE = "public.users";

    public int Id { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime Birthday { get; set; }

    public string Avatar { get; set; }

    public bool IsBanned { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime OnlineDate { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }
  }
}
