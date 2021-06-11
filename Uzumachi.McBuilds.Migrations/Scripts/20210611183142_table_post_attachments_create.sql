CREATE TABLE IF NOT EXISTS "public"."post_attachments" (
  "id" serial4,
  "attachment_type_id" int4 NOT NULL,
  "user_id" int4 NOT NULL,
  "post_id" int4 NOT NULL,
  "value" varchar NOT NULL,
  "params" json,
  "priority" int4 DEFAULT 0,
  "is_deleted" bool DEFAULT false,
  "create_date" timestamp DEFAULT now(),
  "delete_date" timestamp,
  PRIMARY KEY ("id")
)
;