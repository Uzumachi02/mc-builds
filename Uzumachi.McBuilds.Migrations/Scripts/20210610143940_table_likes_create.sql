CREATE TABLE IF NOT EXISTS "public"."likes" (
  "id" serial4,
  "like_type_id" int4 NOT NULL,
  "user_id" int4 NOT NULL,
  "item_id" int4 NOT NULL,
  "create_date" timestamp DEFAULT now(),
  PRIMARY KEY ("id"),
  UNIQUE ("like_type_id", "user_id", "item_id")
)
;