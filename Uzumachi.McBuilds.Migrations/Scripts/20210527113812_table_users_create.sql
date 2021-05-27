CREATE TABLE IF NOT EXISTS "public"."users" (
  "id" serial4,
  "username" varchar,
  "email" varchar NOT NULL,
  "password" varchar NOT NULL,
  "first_name" varchar,
  "last_name" varchar,
  "birthday" date,
  "avatar" varchar,
  "is_banned" bool DEFAULT false,
  "is_deleted" bool DEFAULT false,
  "online_date" timestamp,
  "create_date" timestamp DEFAULT now(),
  "update_date" timestamp DEFAULT now(),
  PRIMARY KEY ("id")
)
;