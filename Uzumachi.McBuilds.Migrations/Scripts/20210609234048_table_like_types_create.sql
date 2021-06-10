CREATE TABLE IF NOT EXISTS "public"."like_types" (
  "id" serial4,
  "name" varchar NOT NULL,
  "create_date" timestamp DEFAULT now(),
  PRIMARY KEY ("id")
)
;