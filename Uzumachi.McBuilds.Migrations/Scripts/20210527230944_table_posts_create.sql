CREATE TABLE IF NOT EXISTS "public"."posts" (
  "id" serial4,
  "user_id" int4 NOT NULL,
  "description" text,
  "view_count" int4 DEFAULT 0,
  "like_count" int4 DEFAULT 0,
  "comment_count" int4 DEFAULT 0,
  "is_deleted" bool DEFAULT false,
  "create_date" timestamp DEFAULT now(),
  "publish_date" timestamp DEFAULT now(),
  "update_date" timestamp DEFAULT now(),
  PRIMARY KEY ("id")
)
;